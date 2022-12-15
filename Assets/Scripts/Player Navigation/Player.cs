using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;

public class Player : MonoBehaviour
{
    [SerializeField] private PlayerInteraction _playerInteraction;
    [SerializeField] private NavigationAroundObject _navigationAroundObject;
    private bool _externalNavigation = true;

    public void SetControllStatus(bool status)
    {
        _externalNavigation = status;

        if(_externalNavigation)
        {
            _navigationAroundObject.SetPitchAndYaw();
        }
    }



    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (!_externalNavigation)
            {
                _externalNavigation = true;
                DOTween.KillAll();
                _navigationAroundObject.SetPitchAndYaw();
            }
        }

        if (_externalNavigation)
        {
            _playerInteraction.TouchHandler();
            _navigationAroundObject.TouchHandler();
        }    
    }


}
