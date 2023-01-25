using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PawObserver : MonoBehaviour
{
    [SerializeField] private List<PawPoint> _activePawPoints = new List<PawPoint>();
    [SerializeField] private float _hitDistance;
    [SerializeField] private LayerMask _pawPointsLayer;
    void Start()
    {

    }

    void Update()
    {

    }
    public void FindNearestsPawPoints(GameObject currentPoint)
    {
        _activePawPoints.Clear();
        Collider[] pawPointColloders = Physics.OverlapSphere(transform.position, _hitDistance, _pawPointsLayer);
        foreach (Collider collider in pawPointColloders)
        {
            if (currentPoint != collider.gameObject)
            {
                _activePawPoints.Add(collider.GetComponent<PawPoint>());
                _activePawPoints[_activePawPoints.Count - 1].SetPointIneractiveStatus();
            }
        }
    }

    public void FindNearestsPawPoints()
    {
        _activePawPoints.Clear();
        Collider[] pawPointColloders = Physics.OverlapSphere(transform.position, _hitDistance, _pawPointsLayer);
        foreach (Collider collider in pawPointColloders)
        {
            _activePawPoints.Add(collider.GetComponent<PawPoint>());
            _activePawPoints[_activePawPoints.Count - 1].SetPointIneractiveStatus();
        }
    }

    public void UpdateToNullPawPointsArray(PawPoint currentPoint)
    {
        if (currentPoint != null)
        {
            foreach (PawPoint pawPoint in _activePawPoints)
            {
                if (currentPoint != pawPoint)
                {
                    pawPoint.ClosePointwWithParticles();
                }
            }
        }
    }

    public void UpdateToNullPawPointsArray()
    {
        foreach (PawPoint pawPoint in _activePawPoints)
        {
            pawPoint.ClosePointwWithParticles();
        }
    }
}
