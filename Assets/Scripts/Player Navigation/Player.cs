using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;
using UnityEngine.AI;

public class Player : MonoBehaviour
{
    [SerializeField] private Rigidbody[] _ragDoll;
    [SerializeField] private PlayerVisualization _playerVisualization;
    [SerializeField] private PawObserver _pawObserver;
    [SerializeField] private InteractionObserver _interactionObserver;
    [SerializeField] private PlayerInteraction _playerInteraction;
    [SerializeField] private NavigationAroundObject _navigationAroundObject;
    [SerializeField] private NavMeshAgent _navMeshAgent;
    [SerializeField] private Transform _closePointToTargetGreen;
    [SerializeField] private Transform _closePointToTargetRed;
    [SerializeField] private Transform _cameraTarget;
    [SerializeField] private float _jumpVectorOffset;
    [SerializeField] private Transform _target;
    private Transform _previousTarget;
    private bool _externalNavigation = true;
    private enum State { BaseState = 0, MoveToFinishPoint = 1, MoveToJumpPoint = 2 }  
    private State _currentState = State.BaseState;


    private void Start()
    {
        foreach (Rigidbody rb in _ragDoll)
        {
            rb.isKinematic = true;
            rb.gameObject.GetComponent<Collider>().enabled = false;
        }

        _pawObserver.FindNearestsPawPoints(_target.gameObject);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (!_externalNavigation)
            {
                _externalNavigation = true;
                _navigationAroundObject.transform.DOKill();
                _navigationAroundObject.SetPitchAndYaw();
            }
        }

        if (_externalNavigation)
        {
            _playerInteraction.TouchHandler();
            _navigationAroundObject.TouchHandler();
        }


        if(_currentState != State.BaseState)
        {
            StateChecker();
        }
    }

    public void SetControllStatus(bool status)
    {
        _externalNavigation = status;

        if (_externalNavigation)
        {
            _navigationAroundObject.SetPitchAndYaw();
        }
    }
    private void StateChecker()
    {
        switch (_currentState)
        {
            
            case State.MoveToFinishPoint:

                if (!_navMeshAgent.pathPending)
                {
                    if (_navMeshAgent.remainingDistance <= _navMeshAgent.stoppingDistance)
                    {
                        if (!_navMeshAgent.hasPath || _navMeshAgent.velocity.sqrMagnitude == 0f)
                        {
                            _target.GetComponent<PawVisualization>().PlayerCame();
                            _pawObserver.FindNearestsPawPoints(_target.gameObject);
                            _interactionObserver.FindNearestsInteractioveObjects();
                            _currentState = State.BaseState;
                            _playerVisualization.SetRunAnimation(false);
                            _playerVisualization.SetJumpAnimation(false);
                        }
                    }
                }
                break;

            case State.MoveToJumpPoint:

                if (!_navMeshAgent.pathPending)
                {
                    if (_navMeshAgent.remainingDistance <= _navMeshAgent.stoppingDistance)
                    {
                        if (!_navMeshAgent.hasPath || _navMeshAgent.velocity.sqrMagnitude == 0f)
                        {
                            _currentState = State.BaseState;
                            _navMeshAgent.enabled = false;
                        }
                    }
                }

                break;
        }

    }

    private void RotationBeforeJump()
    {
        //CheckTargetOrientation(_closePointToTargetRed);
        _cameraTarget.SetParent(null);
        Quaternion targetRotation = Quaternion.LookRotation(new Vector3(_closePointToTargetRed.position.x, transform.position.y, _closePointToTargetRed.position.z) - transform.position);
        transform.DORotateQuaternion(targetRotation, 0.5f).OnComplete(() =>
        {
            _cameraTarget.SetParent(transform);
            _playerVisualization.SetJumpAnimation(true);
            _playerVisualization.SetRunAnimation(false);
        });
    }

    public void RotationForInteraction(Transform interactionObjectTransform, InteractiveObject interactionObject, Vector2 canvasFeedBackPosition)
    {
        _cameraTarget.SetParent(null);
        CheckTargetOrientation(interactionObjectTransform);
        Quaternion targetRotation = Quaternion.LookRotation(new Vector3(interactionObjectTransform.position.x, transform.position.y, interactionObjectTransform.position.z) - transform.position);
        transform.DORotateQuaternion(targetRotation, 0.5f).OnComplete(() =>
        {
            _playerVisualization.SetAttackAnimation();
            StartCoroutine(AttackDeley(interactionObject, canvasFeedBackPosition));
        });
    }
    private IEnumerator AttackDeley(InteractiveObject interactionObject, Vector2 canvasFeedBackPosition)
    {
        yield return new WaitForSeconds(0.7f);
        interactionObject.TakeInteractiveObject(canvasFeedBackPosition);
        _cameraTarget.SetParent(transform);
        _playerVisualization.SetRunAnimation(false);
        _playerVisualization.SetJumpAnimation(false);
    }

    public void Jump()
    {
        transform.DOJump(_closePointToTargetRed.position, 0.6f, 1, 1);
    }
    public void Finish()
    {
        _target.GetComponent<PawVisualization>().PlayerCame();
        _pawObserver.FindNearestsPawPoints(_target.gameObject);
        _interactionObserver.FindNearestsInteractioveObjects();
        _currentState = State.BaseState;
        _playerVisualization.SetRunAnimation(false);
        _playerVisualization.SetJumpAnimation(false);
        _navMeshAgent.enabled = true;
    }


    public void MoveTo(Transform target)
    {

        _target = target;
        _pawObserver.UpdateToNullPawPointsArray(_target.GetComponent<PawPoint>());
        _interactionObserver.UpdateToNullInteractioveObjectsArray();
        CheckTargetOrientation(_target);

        Vector3 pos2 = ReturnClosestPointBackToAgent(_navMeshAgent, target.position);
        Vector3 pos1 = ReturnClosestPointBackToAgent(target.GetComponent<NavMeshAgent>(), pos2);

        _closePointToTargetGreen.position = pos2;
        _closePointToTargetRed.position = pos1;

        if (new Vector3(pos1.x, 0, pos1.z) != new Vector3(pos2.x, 0, pos2.z))
        {
            _navMeshAgent.enabled = false;
            _closePointToTargetRed.position = new Vector3(_target.position.x, _closePointToTargetRed.position.y, _target.position.z);
            RotationBeforeJump();

        }
        else
        {
            _navMeshAgent.SetDestination(_closePointToTargetGreen.position);
            _currentState = State.MoveToFinishPoint;
            CheckTargetOrientation(_target);
        }
        
        target.GetComponent<NavMeshAgent>().enabled = false;
    }

    private void CheckTargetOrientation(Transform target)
    {
        Vector3 directionToTarget = Vector3.Normalize(target.position - transform.position);
        float dot = Vector3.Dot(transform.right, directionToTarget);

        if (Mathf.Abs(dot) <= 0.4f)
        {
            _playerVisualization.SetRunAnimation(true);
            _playerVisualization.SetForwardRun();
        }
        else if (dot < 0)
        {
            _playerVisualization.SetRunAnimation(true);
            _playerVisualization.SetLeftRun();
        }
        else if (dot > 0)
        {
            _playerVisualization.SetRunAnimation(true);
            _playerVisualization.SetRightRun();
        }
    }
    


    public Vector3 ReturnClosestPointBackToAgent(NavMeshAgent navMeshAgent, Vector3 agentPosition)
    {
        NavMeshPath path = new NavMeshPath();
        navMeshAgent.CalculatePath(agentPosition, path);
        var endPointIndex = path.corners.Length - 1;
        return path.corners[endPointIndex];
    }

}
