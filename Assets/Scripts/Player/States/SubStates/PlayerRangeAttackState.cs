using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRangeAttackState : PlayerAbilityState
{
    float fireRateTimeElapse;
    float stopTimeElapse;

    public PlayerRangeAttackState(Core core, Player player, PlayerData data, PlayerStateMachine stateMachine, string animBoolName) : base(core, player, data, stateMachine, animBoolName)
    {

    }

    public override void Enter()
    {
        base.Enter();

        fireRateTimeElapse = 0f;
        stopTimeElapse = 0f;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicsUpdate()
    {
        base.LogicsUpdate();

        fireRateTimeElapse -= Time.deltaTime;

        if (player.inputHandler.rangeAttackInput)
        {
            stopTimeElapse = 0;

            if (fireRateTimeElapse <= 0f)
            {
                fireRateTimeElapse = data.rangedAttackState.fireRate;
                Attack();
            }
        }
        else
        {
            stopTimeElapse += Time.deltaTime;
            if (stopTimeElapse >= data.rangedAttackState.stopTimeAfterFire)
            {
                FinishAttack();
            }
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    void Attack()
    {
        combat.RangeAttack(data.rangedAttackState.attackSpawnData, data.rangedAttackState.projectileData);
    }

    void FinishAttack()
    {
        stateMachine.ChangeState(player.idleState);
    }

    public override void UseInput()
    {
        
    }
}
