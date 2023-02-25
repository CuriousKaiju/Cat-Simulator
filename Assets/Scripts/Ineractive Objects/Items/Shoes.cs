using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoes : InteractiveObject
{
    [SerializeField] private GameObject _poop;

    public override void ClearObject()
    {
        //GameEvents.OnPoop();

        _poop.SetActive(true);
        _collider.enabled = false;
        _interactionPoint.SetActive(false);
        _interactionParticles.SetActive(false);
    }
}
