using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationTriggerReceiver : MonoBehaviour
{
    public Player player;

    private void AnimationTrigger() => player.StateMachine.CurrentState.AnimationTrigger();

    private void AnimationFinishTrigger() => player.StateMachine.CurrentState.AnimationFinishTrigger();
}