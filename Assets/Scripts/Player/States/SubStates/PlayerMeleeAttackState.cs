using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMeleeAttackState : PlayerAbilityState
{
    int index;
    
    public PlayerMeleeAttackState(Core core, Player player, PlayerData data, PlayerStateMachine stateMachine, string animBoolName) : base(core, player, data, stateMachine, animBoolName)
    {

    }

    public override void AnimationFinishTrigger()
    {
        base.AnimationFinishTrigger();

        Attack();
    }

    public override void Enter()
    {
        base.Enter();

        PlayRandomAnimation();
    }

    void PlayRandomAnimation()
    {
        index = Random.Range(0, data.meleeAttackState.attackAnimBoolName.Length);

        player.anim.SetBool(data.meleeAttackState.attackAnimBoolName[index], true);
    }

    public override void Exit()
    {
        player.anim.SetBool(data.meleeAttackState.attackAnimBoolName[index], false);

        base.Exit();
    }

    public override void LogicsUpdate()
    {
        base.LogicsUpdate();
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    void Attack()
    {
        combat.MeleeAttack(data.meleeAttackState.attackSpawnData, data.meleeAttackState.damageData);

        UseInput();
    }

    public override void UseInput()
    {
        player.inputHandler.UseMeleeAttack();
    }
}
