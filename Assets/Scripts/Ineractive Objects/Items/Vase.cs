using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vase : InteractiveObject
{
    [SerializeField] private VaseBroken _vaseBroken;

    public override void ClearObject()
    {
        _vaseBroken.ActivateDestroy();
        _collider.enabled = false;
        _interactionPoint.SetActive(false);
        _interactionParticles.SetActive(false);
    }
}
