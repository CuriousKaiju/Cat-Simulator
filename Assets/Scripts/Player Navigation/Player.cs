using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;
using UnityEngine.AI;

public class Player : MonoBehaviour
{
    [SerializeField] private PlayerInteraction _playerInteraction;
    [SerializeField] private NavigationAroundObject _navigationAroundObject;
    [SerializeField] private NavMeshAgent _navMeshAgent;
    [SerializeField] private Transform _closePointToTargetGreen;
    [SerializeField] private Transform _closePointToTargetRed;
    [SerializeField] private float _jumpVectorOffset;
    private Transform _target;
    private bool _externalNavigation = true;
    private enum State { BaseState = 0, MoveToFinishPoint = 1, MoveToJumpPoint = 2 }
    private State _currentState = State.BaseState; 

    public void SetControllStatus(bool status)
    {
        _externalNavigation = status;

        if (_externalNavigation)
        {
            _navigationAroundObject.SetPitchAndYaw();
        }
    }



    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (!_externalNavigation)
            {
                _externalNavigation = true;
                DOTween.KillAll();
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
                            _navigationAroundObject.SetPitchAndYaw();
                            _currentState = State.BaseState;
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
                            Jump();
                        }
                    }
                }

                break;
        }
    }

    private void Jump()
    {
        transform.DOJump(_closePointToTargetRed.position, 1, 1, 2).OnComplete(() =>
        {
            _navMeshAgent.enabled = true;
            MovementAfterJump();
            _currentState = State.MoveToFinishPoint;
        });
    
    }



    public void MoveTo(Transform target)
    {
        _target = target;

        Vector3 pos2 = ReturnClosestPointBackToAgent(_navMeshAgent, target.position);
        Vector3 pos1 = ReturnClosestPointBackToAgent(target.GetComponent<NavMeshAgent>(), pos2);

        _closePointToTargetGreen.position = pos2;
        _closePointToTargetRed.position = pos1;

        if (new Vector3(pos1.x, 0, pos1.z) != new Vector3(pos2.x, 0, pos2.z))
        {
            _closePointToTargetRed.position = pos1;
            Vector3 shiftVector = new Vector3(pos1.x, 0, pos1.z) - new Vector3(pos2.x, 0, pos2.z);
            _closePointToTargetGreen.position -= shiftVector.normalized * _jumpVectorOffset;
            _closePointToTargetRed.position += shiftVector.normalized * _jumpVectorOffset;

            _navMeshAgent.SetDestination(_closePointToTargetGreen.position);
            _currentState = State.MoveToJumpPoint;
        }
        else
        {
            _navMeshAgent.SetDestination(_closePointToTargetGreen.position);
            _currentState = State.MoveToFinishPoint;
        }

        target.GetComponent<NavMeshAgent>().enabled = false;     
    }

    private void MovementAfterJump()
    {
        _navMeshAgent.SetDestination(_target.position);
    }

    public Vector3 ReturnClosestPointBackToAgent(NavMeshAgent navMeshAgent, Vector3 agentPosition)
    {
        NavMeshPath path = new NavMeshPath();
        navMeshAgent.CalculatePath(agentPosition, path);
        var endPointIndex = path.corners.Length - 1;
        return path.corners[endPointIndex];
    }

}
