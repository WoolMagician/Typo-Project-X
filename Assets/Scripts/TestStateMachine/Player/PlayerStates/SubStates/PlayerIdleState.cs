using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : PlayerGroundedState
{
    public PlayerIdleState(Player player, string animBoolName) : base(player, animBoolName)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();
        player.RB.useGravity = false;
        core.Movement.SetVelocityX(0f);
        core.Movement.SetVelocityY(0f);
    }

    public override void Exit()
    {
        base.Exit();
        player.RB.useGravity = true;
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
        }               
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
