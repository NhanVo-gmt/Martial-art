using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationToStateMachine : MonoBehaviour
{
    public PlayerState playerState;

    public void AnimationTrigger()
    {
        playerState.AnimationTrigger();
    }

    public void AnimationFinishTrigger()
    {
        playerState.AnimationFinishTrigger();
    }
}
