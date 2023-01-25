using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VaseBroken : MonoBehaviour
{
    [SerializeField] private Rigidbody[] _vaseDetails;

    public void ActivateDestroy()
    {
        foreach(Rigidbody rb in _vaseDetails)
        {
            rb.isKinematic = false;
        }
    }


}
