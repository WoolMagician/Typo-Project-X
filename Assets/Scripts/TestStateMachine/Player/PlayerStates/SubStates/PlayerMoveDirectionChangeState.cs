using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveDirectionChangeState : PlayerGroundedState
{
    public PlayerMoveDirectionChangeState(Player player, string animBoolName) : base(player, animBoolName)
    {
    }

    public override void AnimationFinishTrigger()
    {
        base.AnimationFinishTrigger();
        player.Anim.SetBool("moveDirectionChange", false);
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();
        player.Anim.SetBool("moveDirectionChange", true);
    }

    public override void Exit()
    {
        base.Exit();
        player.Anim.SetBool("moveDirectionChange", false);
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (xInput == 0)
        {
            stateMachine.ChangeState(player.IdleState);
        }
        else
        {
            if (isAnimationFinished)
            {
                stateMachine.ChangeState(player.MoveState);
            }
        }
    }
}