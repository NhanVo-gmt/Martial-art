using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Combat : CoreComponent, IDamageable
{
    Player player;
    Movement movement;
    VFXController vfxController;
    BoxCollider2D col;

    public Action<Vector2, AttackDamageData> onTakeDamage;

    Vector2 attackPosition;
    Vector2 attackDirectionVector;
    Quaternion attackRotation;
    
    protected override void Awake()
    {
        base.Awake();

        player = GetComponentInParent<Player>();
        col = GetComponent<BoxCollider2D>();
    }

    void Start() 
    {
        movement = core.GetCoreComponent<Movement>();
        vfxController = core.GetCoreComponent<VFXController>();
    }

    #region Deal Damage

    public void MeleeAttack(AttackSpawnData attackSpawnData, AttackDamageData attackDamageData)
    {
        GetAttackPosition(attackSpawnData);
        GetAttackDirection();

        CheckAndDealMeleeDamage(attackSpawnData, attackDamageData);
    }

    public Vector2 GetAttackPosition(AttackSpawnData attackData)
    {
        switch(player.playerDirection)
        {
            case Direction.Up:
                attackPosition = attackData.upPosition + (Vector2)player.transform.position;
                break;

            case Direction.Down:
                attackPosition = attackData.downPosition + (Vector2)player.transform.position;
                break;

            case Direction.Left:
                attackPosition = attackData.leftPosition + (Vector2)player.transform.position;
                break;

            case Direction.Right:
                attackPosition = attackData.rightPosition + (Vector2)player.transform.position;
                break;

            default:
                break;
        }

        return attackPosition;
    }

    void GetAttackDirection()
    {
        attackDirectionVector = player.playerDirectionVector;
    }

    void CheckAndDealMeleeDamage(AttackSpawnData attackSpawnData, AttackDamageData attackDamageData)
    {
        Collider2D[] collider2DArray = Physics2D.OverlapCircleAll(attackPosition, attackSpawnData.attackRadius);
        foreach (Collider2D collider2D in collider2DArray)
        {
            if (collider2D == col) continue;

            DealDamage(collider2D, attackDirectionVector, attackDamageData);
        }
    }

    public void DealDamage(Collider2D other, Vector2 attackDirection, AttackDamageData attackDamageData)
    {
        IDamageable idamageable = other.gameObject.GetComponent<IDamageable>();
        if (idamageable != null)
        {
            idamageable.TakeDamage(attackDirection, attackDamageData);
        }
    }

    public void RangeAttack(AttackSpawnData attackSpawnData, ProjectileData projectileData)
    {
        GetAttackPosition(attackSpawnData);
        GetAttackRotation(projectileData);

        SpawnProjectile(projectileData);
    }

    void SpawnProjectile(ProjectileData projectileData)
    {
        GameObject projectile = vfxController.SpawnGOWithRotation(projectileData.prefab, attackPosition, attackRotation);
        projectile.GetComponent<Projectile>().Initialize(projectileData, this, vfxController, player.target);
    }

    public void GetAttackRotation(ProjectileData projectileData)
    {
        switch (player.playerDirection)
        {
            case Direction.Up:
                attackRotation = Settings.upRotation;
                break;

            case Direction.Down:
                attackRotation = Settings.downRotation;
                break;

            case Direction.Left:
                attackRotation = Settings.leftRotation;
                break;

            case Direction.Right:
                attackRotation = Settings.rightRotation;
                break;

            default:
                break;
        }

        AddSpreadRotation(projectileData.spreadRotation);
    }

    void AddSpreadRotation(float spreadRotation)
    {
        attackRotation *= Quaternion.Euler(0, 0, UnityEngine.Random.Range(-spreadRotation, spreadRotation));
    }

    #endregion

    #region Take Damage

    public void TakeDamage(Vector2 attackDirecionVector, AttackDamageData attackDamageData)
    {
        onTakeDamage?.Invoke(attackDirecionVector, attackDamageData);
        
        if (player.IsInBlockState())
        {
            //TakeBlockDamage(attackDirecionVector, attackDamage, knockBackStrength);
            return;
        }

        SetPlayerDirectionToAttack(attackDirecionVector);

        KnockBack(attackDirecionVector, attackDamageData);

        SpawnHitVFX();
    }

    void SetPlayerDirectionToAttack(Vector2 attackDirecionVector)
    {
        Direction attackDirection = HelperMethods.GetDirectionFromVector(attackDirecionVector.x, attackDirecionVector.y);
        player.SetPlayerDirection(HelperMethods.GetReverseDirection(attackDirection));
    }

    void KnockBack(Vector2 knockbackDirectionVector, AttackDamageData attackDamageData)
    {
        movement.AddForce(knockbackDirectionVector.normalized * attackDamageData.knockBackStrength, ForceMode2D.Impulse);
    }

    void SpawnHitVFX()
    {
        vfxController.SpawnGOWithRotation(vfxController.data.hitVFX.prefab, vfxController.data.hitVFX.offset + transform.position, Quaternion.identity);
    }

    #endregion
}
