using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveState : PlayerGroundedState
{
    public PlayerMoveState(Player player, string animBoolName) : base(player, animBoolName)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();
        player.RB.drag = 0f;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        //Check if we should flip character based on input
        core.Movement.CheckIfShouldFlip(xInput);

        ////Adjust movement vector to ground slope
        //Vector3 movementVector = core.Movement.AdjustMotionVectorToGroundSlope(core.CollisionSenses.groundNormal, new Vector3(xInput, 0, 0));

        //Set motion vector
        core.Movement.SetVelocityX(playerData.movementVelocity * xInput);

        //If we are not already transitioning to something else and we are standing still go to idle state.
        if (!isExitingState && xInput == 0) stateMachine.ChangeState(player.IdleState);     
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
