using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PawPoint : MonoBehaviour
{
    [HideInInspector] public UnityEvent OnClose;
    private void ClosePoint()
    {
        OnClose.Invoke();
    }
}
