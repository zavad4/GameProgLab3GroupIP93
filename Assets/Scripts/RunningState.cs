using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunningState : State
{
    private Rigidbody _player;

    public RunningState(Rigidbody player)
    {
        _player = player;
    }
    public override void Enter()
    {
        base.Enter();
        Debug.Log("Player is running.");
    }

    public override void Exit()
    {
        base.Exit();
        Debug.Log("Player stopped running.");
    }

    public override void Update()
    {
        base.Update();
        _player.GetComponent<Renderer>().material.color = Color.magenta;
    }
}
