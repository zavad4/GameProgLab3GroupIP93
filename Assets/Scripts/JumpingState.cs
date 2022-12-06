using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpingState : State
{
    private Rigidbody _player;

    public JumpingState(Rigidbody player)
    {
        _player = player;
    }
    public override void Enter()
    {
        base.Enter();
        Debug.Log("before" +_player.rotation.y);
        Quaternion deltaRotation = Quaternion.Euler(new Vector3(0, 4500, 0) * Time.fixedDeltaTime);
        _player.MoveRotation(_player.rotation * deltaRotation);
        Debug.Log("after" +_player.rotation.y);
        Debug.Log("Player is jumping.");
    }

    // Update is called once per frame
    public override void Exit()
    {
        base.Exit();
        Debug.Log("Player landed.");
    }

    public override void Update()
    {
        base.Exit();
        _player.GetComponent<Renderer>().material.color = Color.green;
    }
}
