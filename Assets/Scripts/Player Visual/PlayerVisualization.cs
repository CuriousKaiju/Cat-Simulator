using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerVisualization : MonoBehaviour
{
    [SerializeField] private Animator _playerAnimator;
    void Start()
    {
        
    }
    void Update()
    {
        
    }
    public void SetRunAnimation()
    {
        _playerAnimator.SetTrigger("Run");
    }
    public void SetIdleAnimation()
    {
        _playerAnimator.SetTrigger("Idle");
    }
    public void SetJumpAnimation()
    {
        _playerAnimator.SetTrigger("Jump");
    }
    public void SetLeftRun()
    {
        _playerAnimator.SetTrigger("RunLeft");
    }
    public void SetRightRun()
    {
        _playerAnimator.SetTrigger("RunRight");
    }
}
