using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.AI;
using Lofelt.NiceVibrations;

public class PawPoint : MonoBehaviour
{
    [HideInInspector] public UnityEvent OnClose;

    [SerializeField] private string _desiredTag;
    [SerializeField] private PawVisualization _pawVisualization;
    [SerializeField] private PawPointer _pawPointer;
    [SerializeField] private Collider _collider;
    [SerializeField] private HapticSource _haptic;

    public NavMeshAgent _navMeshAgent;
    public void ClosePoint()
    {
        gameObject.tag = "Untagged";
        SetUnpressedState();
        _pawVisualization.ClosePawPoint();
        OnClose.Invoke();
    }
    public void ClosePointwWithParticles()
    {
        gameObject.tag = "Untagged";
        SetUnpressedState();
        _pawVisualization.CloasePawPointWithParticles();
        OnClose.Invoke();
        _haptic.Play();  
    }
    public void SetPressedState()
    {
        _pawVisualization.SetParticlesStatus(true);
    }
    public void SetUnpressedState()
    {
        _pawVisualization.SetParticlesStatus(false);
    }

    public void SetPointIneractiveStatus()
    {
        GetComponent<NavMeshAgent>().enabled = true;
        gameObject.tag = _desiredTag;
        _pawPointer.InitArrow();
        _pawVisualization.SetActivePawPointStatus();
    }
}
