using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLedgeClimbState : PlayerState
{
    private Vector3 detectedPos;
    private Vector3 cornerPos;
    private Vector3 startPos;
    private Vector3 stopPos;
    private Vector3 workspace;

    private bool isHanging;
    private bool isClimbing;
    private bool jumpInput;
    private bool isTouchingCeiling;
    private bool isWallFront;

    private int xInput;
    private int yInput;

    public PlayerLedgeClimbState(Player player, string animBoolName) : base(player, animBoolName)
    {
    }

    public override void AnimationFinishTrigger()
    {
        base.AnimationFinishTrigger();
        player.Anim.SetBool("climbLedge", false);
    }

    public override void AnimationTrigger()
    {
        base.AnimationTrigger();

        isHanging = true;
    }

    public override void DoChecks()
    {
        base.DoChecks();
        isWallFront = core.CollisionSenses.WallFront;
    }

    public override void Enter()
    {
        base.Enter();

        core.Movement.SetVelocityZero();
        player.transform.position = detectedPos;
        cornerPos = DetermineCornerPosition();

        startPos.Set(cornerPos.x - (core.Movement.FacingDirection * playerData.startOffset.x), cornerPos.y - playerData.startOffset.y, 0f);
        stopPos.Set(cornerPos.x + (core.Movement.FacingDirection * playerData.stopOffset.x), cornerPos.y + playerData.stopOffset.y, 0f);
        stopPos = GetUpdatedStopPosition();

        player.transform.position = startPos;
        player.AnimateCameraChannel.RaiseEvent(core.Movement.FacingDirection);

    }

    public override void Exit()
    {
        base.Exit();

        isHanging = false;

        if (isClimbing)
        {
            player.transform.position = stopPos;
            isClimbing = false;
        }
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (isAnimationFinished)
        {
            if (!isTouchingCeiling)
            {
                stateMachine.ChangeState(player.IdleState);
            }
        }
        else
        {
            xInput = player.InputHandler.NormInputX;
            yInput = player.InputHandler.NormInputY;
            jumpInput = player.InputHandler.JumpInput;

            core.Movement.SetVelocityZero();
            player.transform.position = startPos;

            if (yInput == 1 && isHanging && !isClimbing)
            {
                CheckForSpace();
                isClimbing = true;
                player.Anim.SetBool("climbLedge", true);
            }
            else if (yInput == -1 && isHanging && !isClimbing && isWallFront && isExitingState)
            {
                stateMachine.ChangeState(player.WallGrabState);
            }
            else if (yInput == -1 && isHanging && !isClimbing && isWallFront && !isExitingState)
            {
                stateMachine.ChangeState(player.WallSlideState);
            }
            else if (yInput == -1 && isHanging && !isClimbing)
            {
                stateMachine.ChangeState(player.InAirState);
            }
            else if(jumpInput && !isClimbing)
            {
                player.WallJumpState.DetermineWallJumpDirection(true);
                stateMachine.ChangeState(player.WallJumpState);
            }
        }
    }

    public void SetDetectedPosition(Vector3 pos) => detectedPos = pos;

    private Vector3 GetUpdatedStopPosition()
    {
        Physics.Raycast(stopPos, Vector3.down, out RaycastHit xHit, core.CollisionSenses.WhatIsGround);
        return xHit.point + new Vector3(0, playerData.startOffset.y,0);
    }

    private void CheckForSpace()
    {
        isTouchingCeiling = Physics.Raycast(cornerPos + (Vector3.up * 0.015f) + (Vector3.right * core.Movement.FacingDirection * 0.015f), Vector3.up, playerData.standColliderHeight, core.CollisionSenses.WhatIsGround);
        player.Anim.SetBool("isTouchingCeiling", isTouchingCeiling);
    }

    //private Vector3 DetermineCornerPosition()
    //{
    //    Ray ray1 = new Ray(core.CollisionSenses.WallCheck.position, Vector2.right * core.Movement.FacingDirection);
    //    Ray ray2 = new Ray(core.CollisionSenses.LedgeCheckHorizontal.position + (Vector3)(workspace), Vector2.down);

    //    Physics.Raycast(ray1, out RaycastHit xHit, core.CollisionSenses.WallCheckDistance, core.CollisionSenses.WhatIsGround);
    //    float xDist = xHit.distance;
    //    workspace.Set((xDist + 0.015f) * core.Movement.FacingDirection, 0f, 0f);
    //    Physics.Raycast(ray2, out RaycastHit yHit, core.CollisionSenses.LedgeCheckHorizontal.position.y - core.CollisionSenses.WallCheck.position.y + 0.015f, core.CollisionSenses.WhatIsGround);
    //    float yDist = yHit.distance;

    //    Debug.DrawRay(ray1.origin, ray1.direction, Color.cyan);
    //    Debug.DrawRay(ray2.origin, ray2.direction, Color.yellow);

    //    workspace.Set(core.CollisionSenses.WallCheck.position.x + (xDist * core.Movement.FacingDirection), core.CollisionSenses.LedgeCheckHorizontal.position.y - yDist, 0f);
    //    return workspace;
    //}
    private Vector3 DetermineCornerPosition()
    {
        Physics.Raycast(core.CollisionSenses.WallCheck.position, Vector3.right * core.Movement.FacingDirection, out RaycastHit xHit, core.CollisionSenses.WallCheckDistance, core.CollisionSenses.WhatIsGround);
        float xDist = xHit.distance;
        workspace.Set((xDist + 0.015f) * core.Movement.FacingDirection, 0f, 0f);
        Physics.Raycast(core.CollisionSenses.LedgeCheckHorizontal.position + (Vector3)(workspace), Vector3.down, out RaycastHit yHit, core.CollisionSenses.LedgeCheckHorizontal.position.y - core.CollisionSenses.WallCheck.position.y + 0.015f, core.CollisionSenses.WhatIsGround);
        float yDist = yHit.distance;

        workspace.Set(core.CollisionSenses.WallCheck.position.x + (xDist * core.Movement.FacingDirection), core.CollisionSenses.LedgeCheckHorizontal.position.y - yDist,0f);
        return workspace;
    }

}
