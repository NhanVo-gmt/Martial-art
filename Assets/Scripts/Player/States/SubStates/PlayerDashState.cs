using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MEC;

public class PlayerDashState : PlayerAbilityState
{
    public PlayerDashState(Core core, Player player, PlayerData data, PlayerStateMachine stateMachine, string animBoolName) : base(core, player, data, stateMachine, animBoolName)
    {
        
    }

    public override void AnimationFinishTrigger()
    {
        base.AnimationFinishTrigger();
    }

    public override void Enter()
    {
        base.Enter();

        Timing.RunCoroutine(DashCoroutine());

        SpawnVFX();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicsUpdate()
    {
        
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    private IEnumerator<float> DashCoroutine()
    {
        player.DisableCollider();
        
        movement.SetVelocity(player.playerDirectionVector * data.dashData.dashVelocity);

        while (movement.GetVelocity().magnitude > 0.01f)
        {
            yield return Timing.WaitForOneFrame;

            movement.MultiplyVelocityFactor(data.dashData.dashCoolDown);
        }

        player.EnableCollider();

        UseInput();

        stateMachine.ChangeState(player.idleState);
    }

    public override void SpawnVFX()
    {
        vfxController.SpawnVFX(vfxController.data.dashVFX, player.playerDirection);
        vfxController.SpawnVFX(vfxController.data.dashWindVFX, player.playerDirection);
    }

    public override void UseInput()
    {
        player.inputHandler.UseDash();
    }
}
