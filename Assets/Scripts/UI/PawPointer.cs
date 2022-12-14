using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PawPointer : MonoBehaviour
{
    [SerializeField] private PawPoint _pawPoint;

    private void Start()
    {
        PointerManager.Instance.AddToList(this);
        _pawPoint.OnClose.AddListener(Destroy);
    }
    private void Destroy()
    {
        PointerManager.Instance.RemoveFromList(this);
    }
}
