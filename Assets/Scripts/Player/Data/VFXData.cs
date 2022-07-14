using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "VFXData", menuName = "ScriptableObject/Data/VFXData")]
public class VFXData : ScriptableObject
{
    [Header("Dash")]
    public VFX dashVFX;
    public VFX dashWindVFX;

    [Header("Hit")]
    public VFX hitVFX;

    [Header("Ranged Attack")]
    public VFX kiBlastVFX;
    public VFX explosionVFX;
    public VFX doubleBigExplosionVFX;
    public VFX holeVFX;

    [Header("Super Flight")]
    public VFX airDoubleVFX;
}
