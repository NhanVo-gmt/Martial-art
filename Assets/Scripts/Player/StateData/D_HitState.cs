using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "D_HitState", menuName = "ScriptableObject/Data/D_HitState")]
public class D_HitState : ScriptableObject
{
    public float weakKnockBackRecoverTime;
    public float mediumKnockBackRecoverTime;
    public float strongKnockBackRecoverTime;
    public float velocityDecreaseFactor;

    [Header("VFX Component")]
    public VFX mediumKnockBackVFX;
    public VFX strongKnockBackVFX;
}
