using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MEC;

public class PlayerStunState : PlayerNegativeState
{
    float stunTimeElapse;
    MEC.CoroutineHandle blink;

    
    public PlayerStunState(Core core, Player player, PlayerData data, PlayerStateMachine stateMachine, string animBoolName) : base(core, player, data, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        GetStun();

        blink = Timing.RunCoroutine(BlinkCoroutine(), Segment.FixedUpdate);
    }

    void GetStun() 
    {
        stunTimeElapse = data.stunData.stunTime;

        stateMachine.DisableChangeState();
        player.DisableChangeDirection();
    }

    IEnumerator<float> BlinkCoroutine()
    {
        while(true)
        {
            player.sprite.material = data.stunData.flashMaterial;
            yield return Timing.WaitForSeconds(0.5f);

            player.sprite.material = data.entityData.defaultMaterial;
            yield return Timing.WaitForSeconds(0.5f);
        }
    }


    public override void Exit()
    {
        base.Exit();

        player.ResetOutOfCombatState();
    }

    public override void LogicsUpdate()
    {
        base.LogicsUpdate();

        ReduceStunTime();
    }

    void ReduceStunTime()
    {
        if (stunTimeElapse >= 0)
        {
            stunTimeElapse -= Time.deltaTime;

            if (stunTimeElapse <= 0)
            {
                GetOutOfStun();

                stateMachine.ChangeState(player.idleState);
            }
        }
    }

    void GetOutOfStun()
    {
        stateMachine.EnableChangeState();
        player.EnableChangeDirection();
        
        Timing.KillCoroutines(blink);
        player.sprite.material = data.entityData.defaultMaterial; 
    }


    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    public override void SpawnVFX()
    {
        base.SpawnVFX();
    }

    public override void UseInput()
    {
        base.UseInput();
    }
}
