using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MEC;

[CreateAssetMenu(fileName = "ChargeAttackSkill", menuName = "ScriptableObject/Skill/ChargeAttackSkill")]
public class ChargeAttackSkill : Skill
{    
    [Header("Charge Attack component")]
    [Header("Time component")]
    public float chargeTime;
    public float chargeTimeElapse; 
    public float maxActiveTime;
    public float activeTimeElapse; 


    [Header("Rotate component")]
    public float rotationMaxAngle;
    public float rotationSpeed;
    public float turningWaitTime;


    [Header("Move component")]
    public float chargeVelocity;


    [Header("Attack component")]
    public AttackDamageData minAttackData;
    public AttackDamageData maxAttackData;
    [SerializeField] AttackDamageData attackDataHolder;

    
    [Header("Charging component")]
    CoroutineHandle chargingCoroutineHandle;
    CoroutineHandle turningCoroutineHandle;
    Vector2 movingDirection;

    public override void Activate()
    {
        base.Activate();

        activeTimeElapse = Mathf.Lerp(activeTime, maxActiveTime, chargeTimeElapse / chargeTime);
        
        ActivateCoroutine(player);
    }

    public override void ResetSkillCharging()
    {
        chargeTimeElapse = 0;
        activeTimeElapse = activeTime;
    }


    void ActivateCoroutine(Player player)
    {
        chargingCoroutineHandle = Timing.RunCoroutine(ChargingPlayer(player.target));
        turningCoroutineHandle = Timing.RunCoroutine(Turning(player.target));
    }

    IEnumerator<float> ChargingPlayer(Transform target) 
    {
        player.SetPlayerDirectionToTarget();
        movingDirection = (target.position - player.transform.position).normalized;

        while (true)
        {
            movement.SetVelocity(movingDirection * chargeVelocity);
            yield return Timing.WaitForOneFrame;
        }
    }

    IEnumerator<float> Turning(Transform target)
    {
        while (true)
        {
            float targetAngle = Vector2.SignedAngle(target.position - player.transform.position, movingDirection);

            if (Mathf.Abs(targetAngle) <= rotationMaxAngle)
            {
                if (targetAngle < 0)
                {
                    movingDirection = Quaternion.Euler(0, 0, rotationSpeed) * movingDirection;
                }
                else if (targetAngle > 0)
                {
                    movingDirection = Quaternion.Euler(0, 0, -rotationSpeed) * movingDirection;
                }
            }

            yield return Timing.WaitForSeconds(turningWaitTime);
        }
    }

    public override void Deactivate()
    {
        base.Deactivate();

        movement.StopMoving();

        Timing.KillCoroutines(chargingCoroutineHandle);
        Timing.KillCoroutines(turningCoroutineHandle);
    }

    public override void Charging()
    {
        chargeTimeElapse = Mathf.Clamp(chargeTimeElapse + Time.deltaTime, 0, chargeTime);
    }

    public override void OnTouchPlayer(Collider2D other)
    {
        Attack(other);

        Deactivate();
    }

    void Attack(Collider2D target)
    {
        attackDataHolder.finalAttackDamage = Mathf.Lerp(minAttackData.finalAttackDamage, maxAttackData.finalAttackDamage, chargeTimeElapse / chargeTime);
        attackDataHolder.knockBackStrength = Mathf.Lerp(minAttackData.knockBackStrength, maxAttackData.knockBackStrength, chargeTimeElapse / chargeTime);
        
        combat.DealDamage(target, movingDirection, attackDataHolder);
    }
}
