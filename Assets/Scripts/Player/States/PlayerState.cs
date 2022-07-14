using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerState
{
    protected Core core;

    #region Core component

    protected Movement movement
    {
        get
        {
            if (_movement == null)
            {
                _movement = core.GetCoreComponent<Movement>();
            }
            return _movement;
        }
        private set {}
    }
    private Movement _movement;


    protected Combat combat
    {
        get 
        {
            if (_combat == null)
            {
                _combat = core.GetCoreComponent<Combat>();
            }
            return _combat;
        }
        private set {}
    }
    private Combat _combat;

    protected VFXController vfxController
    {
        get 
        {
            if (_vfxController == null)
            {
                _vfxController = core.GetCoreComponent<VFXController>();
            }
            return _vfxController;
        }
        private set {}
    }
    private VFXController _vfxController;

    protected BuffableEntity buffableEntity
    {
        get 
        {
            if (_buffableEntity == null)
            {
                _buffableEntity = core.GetCoreComponent<BuffableEntity>();
            }
            return _buffableEntity;
        }
        private set {}
    }
    private BuffableEntity _buffableEntity;

    protected SkillController skillController
    {
        get 
        {
            if (_skillController == null)
            {
                _skillController = core.GetCoreComponent<SkillController>();
            }
            return _skillController;
        }
        private set {}
    }
    private SkillController _skillController;

    #endregion

    protected Player player;
    protected PlayerData data;
    protected PlayerStateMachine stateMachine;

    private string animBoolName;
    protected bool isAnimationFinished;

    public PlayerState(Core core, Player player, PlayerData data, PlayerStateMachine stateMachine, string animBoolName)
    {
        this.core = core;
        this.player = player;
        this.data = data;
        this.stateMachine = stateMachine;
        this.animBoolName = animBoolName;

        movement = core.GetCoreComponent<Movement>();
        combat = core.GetCoreComponent<Combat>();
        vfxController = core.GetCoreComponent<VFXController>();
    }

    public virtual void Enter()
    {
        player.atsm.playerState = this;

        player.anim.SetBool(animBoolName, true);

        combat.onTakeDamage += TakeDamage;
    }

    public virtual void Exit() 
    {
        isAnimationFinished = false;
        
        player.anim.SetBool(animBoolName, false);

        combat.onTakeDamage -= TakeDamage;
    }

    public virtual void LogicsUpdate() 
    {

    }

    public virtual void PhysicsUpdate()
    {

    }

    public virtual void TakeDamage(Vector2 attackDirection, AttackDamageData attackDamageData)
    {

    }

    public virtual void AnimationTrigger()
    {

    }

    public virtual void AnimationFinishTrigger()
    {
        isAnimationFinished = true;
    }

    public virtual void UseInput()
    {

    }

    public virtual void SpawnVFX()
    {

    }
}
