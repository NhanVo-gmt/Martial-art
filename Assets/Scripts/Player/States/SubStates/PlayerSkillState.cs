using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkillState : PlayerAbilityState
{
    string skillAnimBoolName;
    
    public PlayerSkillState(Core core, Player player, PlayerData data, PlayerStateMachine stateMachine, string animBoolName) : base(core, player, data, stateMachine, animBoolName)
    {

    }

    public override void Enter()
    {
        base.Enter();

        skillAnimBoolName = skillController.GetSkillAnimName();

        player.anim.SetBool(skillAnimBoolName, true);


        Activate();
    }

    void Activate()
    {
        player.ResetOutOfCombatState();

        skillController.ActivateSkill();
    }

    public override void Exit()
    {
        base.Exit();

        player.anim.SetBool(skillAnimBoolName, false);
    }

    public override void LogicsUpdate()
    {
        base.LogicsUpdate();

        if (skillController.IsSkillFinished())
        {
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
        base.UseInput();
    }
}
