using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    Animator anim;

    void Awake() 
    {
        anim = GetComponentInChildren<Animator>();
    }

    public void SetFloat(string animName, float value)
    {
        anim.SetFloat(animName, value);
    }

    public void SetBool(string animName, bool value)
    {
        anim.SetBool(animName, value);
    }
}
