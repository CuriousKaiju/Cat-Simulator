using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laptop : InteractiveObject
{
    [SerializeField] private GameObject _basicLaptop;
    [SerializeField] private GameObject _laptopAfterExplosion;
    public override void ClearObject()
    {
        _basicLaptop.SetActive(false);
        _laptopAfterExplosion.SetActive(true);
        _collider.enabled = false;
        _interactionPoint.SetActive(false);
        _interactionParticles.SetActive(false);
    }
}
