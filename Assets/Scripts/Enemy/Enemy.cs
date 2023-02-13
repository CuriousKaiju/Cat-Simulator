using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.AI;
public class Enemy : MonoBehaviour
{
    [SerializeField] private Transform _door;
    [SerializeField] private float _timeForOpenTheDoor;
    [SerializeField] private Vector3 _doorVector;
    [SerializeField] private Transform _finishPoint;
    [SerializeField] private NavMeshAgent _navMeshAgent;
    [SerializeField] private Animator _animator;
    [SerializeField] private Transform _player;
    [SerializeField] private NavMeshAgent _finishNavMeshAgent;
    [SerializeField] private Collider _collider;
    [SerializeField] private Player _playerScript;
    [SerializeField] private GameObject _angryEmoji;
    [SerializeField] private GameObject _hitParticles;

    private bool _movementPhase = true;
    private void Awake()
    {
        GameEvents.OnPoop += InitMovement;
    }
    private void OnDestroy()
    {
        GameEvents.OnPoop -= InitMovement;
    }

    private void Finish()
    {
        _collider.enabled = false;
        _navMeshAgent.enabled = false;
        transform.DOLookAt(new Vector3(_player.position.x, transform.position.y, _player.position.z), 0.5f).OnComplete(() =>
        {
            _animator.SetTrigger("Attack");
            StartCoroutine(Lose());
        });
    }
    
    IEnumerator Lose()
    {
        yield return new WaitForSeconds(0.5f);
        //_hitParticles.SetActive(true);
        _playerScript.SetLostStatus();
    }
    private void Update()
    {
        if(new Vector3(transform.position.x, 0, transform.position.z) == new Vector3(_finishPoint.position.x, 0, _finishPoint.position.z) && _movementPhase)
        {
            Finish();
            _movementPhase = false;
        }
    }
    private void InitMovement()
    {
        OpenDoor();
        SetDestination();
    }

    private void OpenDoor()
    {
        _door.DORotate(_doorVector, _timeForOpenTheDoor);
    }

    private void SetDestination()
    {
        //_angryEmoji.SetActive(true);
        _navMeshAgent.SetDestination(_finishPoint.position);
        _finishNavMeshAgent.enabled = false;
    }
}
