using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BottomPanel : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    private bool _state;

    public void ChangePanelPosition()
    {
        _state = !_state;

        if(_state)
        {
            _animator.SetTrigger("Down");
        }
        else
        {
            _animator.SetTrigger("Up");
        }
    }
}
