using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAuraState : PlayerAbilityState
{
    float timeToFinish = 0;
    
    public PlayerAuraState(Core core, Player player, PlayerData data, PlayerStateMachine stateMachine, string animBoolName) : base(core, player, data, stateMachine, animBoolName)
    {
    }


    public override void Enter()
    {
        base.Enter();

        player.aura.SetActive(true);

        timeToFinish = data.auraData.chargingTime;
    }


    public override void Exit()
    {
        base.Exit();

        player.aura.SetActive(false);
    }


    public override void LogicsUpdate()
    {
        base.LogicsUpdate();

        timeToFinish -= Time.deltaTime;

        CheckingIfFinishCharging();

        if (!player.inputHandler.auraInput)
        {
            stateMachine.ChangeState(player.idleState);
        }
    }

    void CheckingIfFinishCharging()
    {
        if (timeToFinish <= 0)
        {
            buffableEntity.AddBuff(data.auraData.abilityFastCoolDownBuff);
            UseInput();
            
            stateMachine.ChangeState(player.idleState);
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
        player.inputHandler.UseAuraInput();
    }
}
