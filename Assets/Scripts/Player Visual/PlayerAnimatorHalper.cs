using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimatorHalper : MonoBehaviour
{
   
    [Header("COMPONENTS")]
    [SerializeField] private Player _player;

    public void PlayerJump()
    {
        _player.Jump();
    }
    public void PlayerAfterJump()
    {
        _player.Finish();
    }
}
