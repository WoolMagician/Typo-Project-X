using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallJumpState : PlayerAbilityState
{
    private int wallJumpDirection;

    public PlayerWallJumpState(Player player, string animBoolName) : base(player, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        player.InputHandler.UseJumpInput();
        player.JumpState.ResetAmountOfJumpsLeft();
        core.Movement.SetVelocity(playerData.wallJumpVelocity * (player.InputHandler.NormInputX == core.Movement.FacingDirection ? 0.5f : 1f), playerData.wallJumpAngle, wallJumpDirection);

        //if (player.InputHandler.NormInputX == 0)
        //{
        //    core.Movement.SetVelocity(playerData.wallJumpVelocity, new Vector2((playerData.wallJumpAngle.x / 2f) * wallJumpDirection, playerData.wallJumpAngle.y));
        //}
        //else if (Mathf.Sign(player.InputHandler.NormInputX) == Mathf.Sign(core.Movement.FacingDirection))
        //{
        //    core.Movement.SetVelocity(playerData.wallJumpVelocity, new Vector2(0.5f * wallJumpDirection, playerData.wallJumpAngle.y));
        //}
        //else
        //{
        //    core.Movement.SetVelocity(playerData.wallJumpVelocity, new Vector2(playerData.wallJumpAngle.x * wallJumpDirection, playerData.wallJumpAngle.y));
        //}
        core.Movement.CheckIfShouldFlip(wallJumpDirection);
        player.JumpState.DecreaseAmountOfJumpsLeft();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        player.Anim.SetFloat("yVelocity", core.Movement.CurrentVelocity.y);
        player.Anim.SetFloat("xVelocity", Mathf.Abs(core.Movement.CurrentVelocity.x));

        if (Time.time >= startTime + playerData.wallJumpTime)
        {
            isAbilityDone = true;
        }
    }

    public void DetermineWallJumpDirection(bool isTouchingWall)
    {
        if (isTouchingWall)
        {
            wallJumpDirection = -core.Movement.FacingDirection;
        }
        else
        {
            wallJumpDirection = core.Movement.FacingDirection;
        }
    }
}
