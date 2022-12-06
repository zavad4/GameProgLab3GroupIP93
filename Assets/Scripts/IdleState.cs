using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : State
{
    private Rigidbody _player;
    public IdleState(Rigidbody player)
    {
        _player = player;
    }
    public override void Enter()
    {
        base.Enter();
        Debug.Log("Player is in idle state");
        _player.GetComponent<Renderer>().material.color = Color.white;
    }

    public override void Exit()
    {
        base.Exit();
        Debug.Log("Player start moving.");
    }
}
