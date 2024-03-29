﻿using UnityEngine;

public class PlayerInAirState : PlayerState
{
    //Input
    private int xInput;
    private bool jumpInput;
    private bool jumpInputStop;
    private bool grabInput;
    private bool attackInput;
    private bool attackInputStop;

    //Checks
    private bool isGrounded;
    private bool isGroundedOnGrabbable;
    private bool isTouchingWall;
    private bool isTouchingWallBack;
    private bool isTouchingCeiling;
    private bool oldIsTouchingWall;
    private bool oldIsTouchingWallBack;
    private bool isTouchingLedge;
    private bool isTouchingSwing;

    private bool coyoteTime;
    private bool wallJumpCoyoteTime;
    private bool isJumping;

    private float startWallJumpCoyoteTime;

    public PlayerInAirState(Player player, string animBoolName) : base(player, animBoolName)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();

        oldIsTouchingWall = isTouchingWall;
        oldIsTouchingWallBack = isTouchingWallBack;

        isGrounded = core.CollisionSenses.IsGrounded();
        isGroundedOnGrabbable = core.CollisionSenses.IsGroundedOnGrabbable() && 
                                core.CollisionSenses.CanLandOnGrabbableObject();
        isTouchingWall = core.CollisionSenses.WallFront;
        isTouchingWallBack = core.CollisionSenses.WallBack;
        isTouchingLedge = core.CollisionSenses.LedgeHorizontal;
        isTouchingCeiling = core.CollisionSenses.Ceiling;
        isTouchingSwing = core.CollisionSenses.Swing;

        if (isTouchingSwing)
        {
            player.SwingGrabState.SetDetectedPosition(player.transform.position);
        }

        if (isTouchingWall && !isTouchingLedge)
        {
            player.LedgeClimbState.SetDetectedPosition(player.transform.position);
        }
        if(!wallJumpCoyoteTime && !isTouchingWall && !isTouchingWallBack && (oldIsTouchingWall || oldIsTouchingWallBack))
        {
            StartWallJumpCoyoteTime();
        }
    }

    public override void Enter()
    {
        base.Enter();
        player.AnimateCameraChannel.RaiseEvent(0, 0);
    }

    public override void Exit()
    {
        base.Exit();

        oldIsTouchingWall = false;
        oldIsTouchingWallBack = false;
        isTouchingWall = false;
        isTouchingWallBack = false;
        isTouchingSwing = false;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        CheckCoyoteTime();
        CheckWallJumpCoyoteTime();

        xInput = player.InputHandler.NormInputX;
        jumpInput = player.InputHandler.JumpInput;
        jumpInputStop = player.InputHandler.JumpInputStop;
        grabInput = player.InputHandler.GrabInput;
        attackInput = player.InputHandler.AttackInput;

        CheckJumpMultiplier();

        if (isGrounded && !isJumping && !isGroundedOnGrabbable)
        {            
            stateMachine.ChangeState(player.LandState);
        }
        else if(isGrounded && isJumping && core.Movement.CurrentVelocity.y < 0.01f && !isGroundedOnGrabbable)
        {
            stateMachine.ChangeState(player.LandState);
        }
        else if(isGroundedOnGrabbable && !isJumping)
        {            
            player.LandOnGrabbableState.SetGrabbedObject(core.CollisionSenses.groundCollider.GetComponent<IGrabbable>());
            stateMachine.ChangeState(player.LandOnGrabbableState);
        }
        else if (isGroundedOnGrabbable && isJumping && core.Movement.CurrentVelocity.y < 0.01f)
        {
            player.LandOnGrabbableState.SetGrabbedObject(core.CollisionSenses.groundCollider.GetComponent<IGrabbable>());
            stateMachine.ChangeState(player.LandOnGrabbableState);
        }
        else if(isTouchingSwing)
        {
            stateMachine.ChangeState(player.SwingGrabState);
        }
        else if (isTouchingWall && !isTouchingLedge && !isGrounded && !isTouchingSwing)
        {
            stateMachine.ChangeState(player.LedgeClimbState);
        }
        else if(jumpInput && (isTouchingWall || isTouchingWallBack || wallJumpCoyoteTime))
        {
            StopWallJumpCoyoteTime();
            isTouchingWall = core.CollisionSenses.WallFront;
            player.WallJumpState.DetermineWallJumpDirection(isTouchingWall);
            stateMachine.ChangeState(player.WallJumpState);
        }
        else if(jumpInput && player.JumpState.CanJump())
        {
            stateMachine.ChangeState(player.JumpState);
        }
        else if(isTouchingWall && grabInput && isTouchingLedge)
        {
            stateMachine.ChangeState(player.WallGrabState);
        }
        else if(isTouchingWall && xInput == core.Movement.FacingDirection && core.Movement.CurrentVelocity.y <= 0.9f)
        {
            stateMachine.ChangeState(player.WallGrabState);
        }
        else
        {
            if (isTouchingCeiling)
            {
                core.Movement.SetVelocityY(-0.5f);
            }
            core.Movement.SetVelocityX(playerData.airVelocity * xInput);

            if (!isExitingState)
            {
                player.Anim.SetFloat("yVelocity", core.Movement.CurrentVelocity.y);
                player.Anim.SetFloat("xVelocity", Mathf.Abs(core.Movement.CurrentVelocity.x));
                core.Movement.CheckIfShouldFlip(xInput);

                if (attackInput && !attackInputStop)
                {
                    stateMachine.ChangeState(player.ThrowWeaponState);
                }
            }
        }
    }

    private void CheckJumpMultiplier()
    {
        if (isJumping)
        {
            if (jumpInputStop)
            {
                core.Movement.SetVelocityY(core.Movement.CurrentVelocity.y * playerData.variableJumpHeightMultiplier);
                isJumping = false;
            }
            else if (core.Movement.CurrentVelocity.y <= 0f)
            {
                isJumping = false;
            }

        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    private void CheckCoyoteTime()
    {
        if(coyoteTime && Time.time > startTime + playerData.coyoteTime)
        {
            coyoteTime = false;
            player.JumpState.DecreaseAmountOfJumpsLeft();
        }
    }

    private void CheckWallJumpCoyoteTime()
    {
        if(wallJumpCoyoteTime && Time.time > startWallJumpCoyoteTime + playerData.coyoteTime)
        {
            wallJumpCoyoteTime = false;
        }
    }

    public void StartCoyoteTime() => coyoteTime = true;

    public void StartWallJumpCoyoteTime()
    {
        wallJumpCoyoteTime = true;
        startWallJumpCoyoteTime = Time.time;
    }

    public void StopWallJumpCoyoteTime() => wallJumpCoyoteTime = false;

    public void SetIsJumping() => isJumping = true;
}