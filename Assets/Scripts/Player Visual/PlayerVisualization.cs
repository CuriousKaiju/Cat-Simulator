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
    public void SetRunAnimation(bool runStatus)
    {
        _playerAnimator.SetBool("Run", runStatus);
    }
    public void SetJumpAnimation(bool jumpStatus)
    {
        _playerAnimator.SetBool("Jump", jumpStatus);
    }


    public void SetLeftRun()
    {
        _playerAnimator.SetTrigger("RunLeft");
    }
    public void SetRightRun()
    {
        _playerAnimator.SetTrigger("RunRight");
    }

    public void SetForwardRun()
    {
        _playerAnimator.SetTrigger("RunForward");
    }

    public void SetAttackAnimation()
    {
        _playerAnimator.SetTrigger("Attack");
    }

    public void SetPoopAnimation()
    {
        _playerAnimator.SetTrigger("Poop");
    }

    public void SetClawsAnimation()
    {
        _playerAnimator.SetTrigger("Sharpens");
    }

    public void OffAnimation()
    {
        _playerAnimator.enabled = false;
    }
}
