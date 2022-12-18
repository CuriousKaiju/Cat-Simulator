using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PawPointer : MonoBehaviour
{
    [SerializeField] private PawPoint _pawPoint;

    private void Start()
    {
        _pawPoint.OnClose.AddListener(Destroy);
    }
    public void InitArrow()
    {
        PointerManager.Instance.AddToList(this);
    }
    private void Destroy()
    {
        PointerManager.Instance.RemoveFromList(this);
    }
}
