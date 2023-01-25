using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Claws : InteractiveObject
{
    [SerializeField] private GameObject _additionalClaws;
    public override void ClearObject()
    {
        _additionalClaws.SetActive(true);
        _collider.enabled = false;
        _interactionPoint.SetActive(false);
        _interactionParticles.SetActive(false);
    }
}
