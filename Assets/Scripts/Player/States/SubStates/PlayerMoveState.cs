using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveState : PlayerPassiveState
{
    public PlayerMoveState(Core core, Player player, PlayerData data, PlayerStateMachine stateMachine, string animBoolName) : base(core, player, data, stateMachine, animBoolName)
    {
    }

    public override void AnimationFinishTrigger()
    {
        base.AnimationFinishTrigger();
    }

    public override void Enter()
    {
        base.Enter();
    }


    public override void Exit()
    {
        base.Exit();
    }


    public override void LogicsUpdate()
    {
        base.LogicsUpdate();

        if (movementInput.magnitude == 0)
        {
            stateMachine.ChangeState(player.idleState);
        }
        else if (data.entityData.isSuperFlight)
        {
            stateMachine.ChangeState(player.superFlightState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();

        Move();
    }

    void Move() 
    {
        movement.Move(movementInput * data.moveData.movementSpeed);
    }
}
