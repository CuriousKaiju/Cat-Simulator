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
}
