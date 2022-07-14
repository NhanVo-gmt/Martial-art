using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "D_MeleeAttackState", menuName = "ScriptableObject/Data/D_MeleeAttackState")]
public class D_MeleeAttackState : ScriptableObject
{
    [Header("Attack Component")]
    public string[] attackAnimBoolName;
    public AttackSpawnData attackSpawnData;
    public AttackDamageData damageData;

    [Header("Knockback Component")]
    public float knockBackStrength;

    [Header("Time cooldown")]
    public int changeStateTime;

    [Header("VFX component")]
    public VFX punchVFX; 
    public VFX kickVFX; 
    public VFX hitVFX; 

}
