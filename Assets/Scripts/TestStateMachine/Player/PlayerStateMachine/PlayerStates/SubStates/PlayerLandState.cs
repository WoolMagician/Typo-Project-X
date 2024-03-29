﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLandState : PlayerGroundedState
{
    public PlayerLandState(Player player, string animBoolName) : base(player, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        player.Anim.SetFloat("yVelocity", 0);
        player.Anim.SetInteger("landType", 0);
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (!isExitingState)
        {
            if (xInput != 0)
            {
                stateMachine.ChangeState(player.MoveState);
            }
            else
            {
                stateMachine.ChangeState(player.IdleState);
            }
        }       
    }
}
