using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Skill : ScriptableObject
{
    [Header("Basic component")] 
    public string animName;
    public float coolDownTime;
    public float activeTime;

    [Header("Bool component")]
    public bool needTarget; 
    public bool isEmergency; 
    public bool needCharging; 

    [Header("Editor component")]
    public bool isPlaying = false;
    public float timePlaying;
    public bool isCoolDown;
    public float coolDownTimeElapse;

    protected Player player;
    protected Movement movement;
    protected Combat combat;
    protected VFXController vfxController;

    public virtual void Initialize(Player player, Core core)
    {
        this.player = player;
        GetCoreComponent(core);
    }

    public virtual void GetCoreComponent(Core core)
    {
        movement = core.GetCoreComponent<Movement>();
        combat = core.GetCoreComponent<Combat>();
        vfxController = core.GetCoreComponent<VFXController>();
    }

    public virtual void Activate()
    {
        ResetSkill();
    }


    public virtual void ResetSkill() 
    {
        isPlaying = true;
        timePlaying = activeTime;
    }
    
    public virtual void Cooldown(float delta)
    {
        if (coolDownTimeElapse > 0)
        {
            coolDownTimeElapse -= delta;
            if (coolDownTimeElapse <= 0)
            {
                isCoolDown = false;
            }
        }
    }

    public virtual void LogicsUpdate()
    {
        timePlaying -= Time.deltaTime;

        if (timePlaying <= 0)
        {
            Deactivate();
        }
    }
    
    public virtual void Deactivate()
    {
        isPlaying = false;

        BeginCooldown();
    }

    public virtual void BeginCooldown()
    {
        isCoolDown = true;
        coolDownTimeElapse = coolDownTime;
    }

    public virtual void PhysicsUpdate(){}

    public virtual void ResetSkillCharging(){}


    public virtual void Charging(){}

    public virtual void OnTouchPlayer(Collider2D other) {}

    public virtual void SpawnVFX(){}
}
