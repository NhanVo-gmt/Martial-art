using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    ProjectileData projectileData;
    Rigidbody2D rb;
    Combat combat;
    VFXController vfxController;
    SpawnableObject vfxAnim; // Used for release gameobject;

    Transform target;
    bool targetInRange;


    void Awake() 
    {
        rb = GetComponent<Rigidbody2D>();
        vfxAnim = GetComponent<SpawnableObject>();
    }

    public void Initialize(ProjectileData projectileData, Combat combat, VFXController vfxController, Transform target = null)
    {
        this.projectileData = projectileData;
        this.combat = combat;
        this.vfxController = vfxController;
        this.target = target;

        StartCoroutine(ExploseCoroutine(projectileData.liveTime));
    }

    private void FixedUpdate() 
    {
        Homing();
        Move();
    }

    void Move() 
    {
        rb.velocity = transform.right * projectileData.velocity * Time.deltaTime;
    }

    void Homing()
    {
        if (target != null)
        {
            float targetAngle = Vector2.SignedAngle(target.position - transform.position, transform.right);

            if (CheckingIfTargetInRange(targetAngle))
            {
                RotateProjectile(targetAngle);
            }
        }
    }

    bool CheckingIfTargetInRange(float angle)
    {
        return Mathf.Abs(angle) <= projectileData.rotationMaxAngle;
    }

    void RotateProjectile(float angle)
    {
        if (angle < 0)
        {
            transform.Rotate(0, 0, projectileData.rotationSpeed * Time.deltaTime);
        }
        else if (angle > 0)
        {
            transform.Rotate(0, 0, -projectileData.rotationSpeed * Time.deltaTime);
        }
    }


    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject != combat.gameObject)
        {
            DealDamage(other);
        }
    }

    void DealDamage(Collider2D other)
    {
        if (other.TryGetComponent<IDamageable>(out IDamageable target))
        {
            target.TakeDamage(transform.right, projectileData.attackDamageData);
            Explose();
        }
    }

    void Explose()
    {
        vfxController.SpawnGOWithRotation(vfxController.data.explosionVFX.prefab, transform.position, Quaternion.identity);

        vfxController.SpawnGOWithRotation(vfxController.data.doubleBigExplosionVFX.prefab, transform.position + vfxController.data.doubleBigExplosionVFX.offset, Quaternion.identity);

        vfxAnim.ReleaseObject();
    }

    void ExploseWithoutHit()
    {
        vfxController.SpawnGOWithRotation(vfxController.data.explosionVFX.prefab, transform.position, Quaternion.identity);

        vfxController.SpawnGOWithRotation(vfxController.data.doubleBigExplosionVFX.prefab, transform.position + vfxController.data.doubleBigExplosionVFX.offset, Quaternion.identity);

        vfxController.SpawnGOWithRotation(vfxController.data.holeVFX.prefab, transform.position + vfxController.data.holeVFX.offset, Quaternion.identity);

        vfxAnim.ReleaseObject();
    }

    IEnumerator ExploseCoroutine(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);

        ExploseWithoutHit();
    }
}
