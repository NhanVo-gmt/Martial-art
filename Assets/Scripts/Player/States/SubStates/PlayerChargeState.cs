using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerChargeState : PlayerAbilityState
{
    public PlayerChargeState(Core core, Player player, PlayerData data, PlayerStateMachine stateMachine, string animBoolName) : base(core, player, data, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        player.aura.SetActive(true);
        player.ResetAuraTransform();
        skillController.ResetSkillCharging();
    }


    public override void Exit()
    {
        base.Exit();

        player.aura.SetActive(false);
    }


    public override void LogicsUpdate()
    {
        base.LogicsUpdate();

        skillController.ChargeSkill();

        if (!player.inputHandler.skillInput)
        {
            stateMachine.ChangeState(player.skillState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    public override void SpawnVFX()
    {
        base.SpawnVFX();
    }

    public override void TakeDamage(Vector2 attackDirection, AttackDamageData attackDamageData)
    {
        base.TakeDamage(attackDirection, attackDamageData);
    }

    public override void UseInput()
    {
        base.UseInput();
    }
}
