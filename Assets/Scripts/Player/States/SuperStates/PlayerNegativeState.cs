using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerNegativeState : PlayerState
{
    public PlayerNegativeState(Core core, Player player, PlayerData data, PlayerStateMachine stateMachine, string animBoolName) : base(core, player, data, stateMachine, animBoolName)
    {

    }

    public override void Enter()
    {
        base.Enter();

        stateMachine.DisableChangeState();
    }


    public override void Exit()
    {
        base.Exit();

        stateMachine.EnableChangeState();
    }


    public override void LogicsUpdate()
    {
        base.LogicsUpdate();
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    public override void SpawnVFX()
    {
        base.SpawnVFX();
    }
}
