using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunningState : IState
{
    private PlayerMachine player;
    private bool shootingActive;
    public RunningState(PlayerMachine _player)
    {
        player = _player;
    }
    public void OnEnter()
    {
        player.animator.SetBool("IsRunning", true);
    }
    public void OnUpdate()
    {
        if (GameManager.Instance.canPlayerMove)
        {
            player.playerMove.Move();
        }
    }
    public void OnExit()
    {
        player.animator.SetBool("IsRunning", false);
    }
}
