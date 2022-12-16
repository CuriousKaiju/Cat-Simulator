using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.AI;

public class PawPoint : MonoBehaviour
{
    [HideInInspector] public UnityEvent OnClose;

    [SerializeField] private PawVisualization _pawVisualization;
    public NavMeshAgent _navMeshAgent;
    public void ClosePoint()
    {
        SetUnpressedState();
        _pawVisualization.ClosePawPoint();
        OnClose.Invoke();
    }
    public void SetPressedState()
    {
        _pawVisualization.SetParticlesStatus(true);
    }
    public void SetUnpressedState()
    {
        _pawVisualization.SetParticlesStatus(false);
    }
}
