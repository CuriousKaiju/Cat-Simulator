using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Player : MonoBehaviour
{
    [SerializeField] private PlayerInteraction _playerInteraction;
    [SerializeField] private NavigationAroundObject _navigationAroundObject;
    private bool _externalNavigation;

    public void SetControllStatus(bool status)
    {
        _playerInteraction.enabled = status;
        _navigationAroundObject.enabled = status;
        _navigationAroundObject.SetPitchAndYaw();
    }

   

    private void Update()
    {
        if(_externalNavigation)
        {
            _playerInteraction.TouchHandler();
            _navigationAroundObject.TouchHandler();
        }
    }


}
