using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAbilityState : PlayerState
{    
    public PlayerAbilityState(Core core, Player player, PlayerData data, PlayerStateMachine stateMachine, string animBoolName) : base(core, player, data, stateMachine, animBoolName)
    {
    }

    public override void AnimationFinishTrigger()
    {
        base.AnimationFinishTrigger();
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
        player.inputHandler.ResetInput();
    }

    public override void LogicsUpdate()
    {
        if (isAnimationFinished)
        {
            stateMachine.ChangeState(player.idleState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    public override void TakeDamage(Vector2 attackDirection, AttackDamageData attackDamageData)
    {
        player.hitState.SetKnockbackType(HelperMethods.GetKnockBackType(attackDamageData.knockBackStrength));
        stateMachine.ChangeState(player.hitState);
    }
}
