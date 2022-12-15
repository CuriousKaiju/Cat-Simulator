using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private PlayerInteraction _playerInteraction;
    [SerializeField] private NavigationAroundObject _navigationAroundObject;

    private void Start()
    {
        Cursor.visible = true;
    }

    public void SetControllStatus(bool status)
    {
        if (status)
        {
            CameraRotationToNull();
        }
        _playerInteraction.enabled = status;
        _navigationAroundObject._externalRotationPhase = status;
    }
    private void CameraRotationToNull()
    {
        _navigationAroundObject.SetPitchAndYaw();
    }
    private void Update()
    {
        
    }


}
