using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBlockState : PlayerAbilityState
{
    public PlayerBlockState(Core core, Player player, PlayerData data, PlayerStateMachine stateMachine, string animBoolName) : base(core, player, data, stateMachine, animBoolName)
    {
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
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();

        if (!player.inputHandler.blockInput)
        {
            stateMachine.ChangeState(player.idleState);
        }
    }

    public override void SpawnVFX()
    {
        
    }

    public override void UseInput()
    {
        
    }

    public override void TakeDamage(Vector2 attackDirection, AttackDamageData attackDamageData)
    {
        if (CheckingIfAttackFromBehind(attackDirection))
        {
            stateMachine.ChangeState(player.hitState);
        }
        else
        {
            TakeBlockingGaugeDamage(attackDamageData);
        }
    }

    void TakeBlockingGaugeDamage(AttackDamageData attackDamageData)
    {
        data.entityData.blockingCurrentGauge -= attackDamageData.blockingGaugeDamage;

        if (data.entityData.blockingCurrentGauge <= 0)
        {
            stateMachine.ChangeState(player.stunState);
        }
    }
    

    bool CheckingIfAttackFromBehind(Vector2 attackDirection)
    {
        return Vector2.Angle(player.playerDirectionVector, attackDirection) < 45;
    }
}
