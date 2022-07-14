using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MEC;

public class PlayerHitState : PlayerNegativeState
{
    float hitTimeCoolDown;
    KnockBackType knockBackType;
    
    public PlayerHitState(Core core, Player player, PlayerData data, PlayerStateMachine stateMachine, string animBoolName) : base(core, player, data, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        Initialize();
    }

    void Initialize()
    {
        if (knockBackType == KnockBackType.weak)
        {
            hitTimeCoolDown = data.hitData.weakKnockBackRecoverTime;
            return;
        }
        else if (knockBackType == KnockBackType.medium)
        {
            hitTimeCoolDown = data.hitData.mediumKnockBackRecoverTime;
            //vfxToSpawn = d_HitState.mediumKnockBackVFX;
        }
        else
        {
            hitTimeCoolDown = data.hitData.strongKnockBackRecoverTime;
            //vfxToSpawn = d_HitState.strongKnockBackVFX;
        }
    }

    public override void TakeDamage(Vector2 attackDirection, AttackDamageData attackDamageData)
    {
        Initialize();
    }


    public override void Exit()
    {
        base.Exit();
    }


    public override void LogicsUpdate()
    {
        base.LogicsUpdate();

        UsingEmergencySkill();

        ReduceHitTime();
    }

    void UsingEmergencySkill()
    {
        if (player.inputHandler.skillInput)
        {
            if (skillController.IsSkillEmergency())
            {
                stateMachine.ChangeState(player.skillState);
            }
        }
    }

    void ReduceHitTime()
    {
        if (hitTimeCoolDown >= 0)
        {
            hitTimeCoolDown -= Time.deltaTime;

            if (hitTimeCoolDown <= 0)
            {
                GetOutOfHit();
            }
        }
    }

    void GetOutOfHit()
    {
        stateMachine.ChangeState(player.idleState);
    }


    bool CheckingToStartRecover()
    {
        return knockBackType == KnockBackType.strong && hitTimeCoolDown <= data.hitData.strongKnockBackRecoverTime / 2;
    }

    IEnumerator<float> SlowingDownCharacter()
    {
        float decreaseVelocityFactor = data.hitData.velocityDecreaseFactor;

        while (movement.GetVelocity().magnitude > 0.01f)
        {
            movement.MultiplyVelocityFactor(decreaseVelocityFactor);
            yield return Timing.WaitForOneFrame;
        }
    }



    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    public override void SpawnVFX()
    {
        
    }

    public void SetKnockbackType(KnockBackType knockBackType)
    {
        this.knockBackType = knockBackType;
    }
}
