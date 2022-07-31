using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSwingGrabState : PlayerState
{
    private int xInput;
    private bool jumpInput;
    private Vector3 detectedPos;
    private Vector3 swingPos;
    private Vector3 startPos;
    private Vector3 workspace;

    private Quaternion originalPlayerRotation;

    public PlayerSwingGrabState(Player player, string animBoolName) : base(player, animBoolName)
    {
    }

    public override void AnimationFinishTrigger()
    {
        base.AnimationFinishTrigger();
    }

    public override void AnimationTrigger()
    {
        base.AnimationTrigger();
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();

        originalPlayerRotation = player.transform.rotation;

        player.RB.isKinematic = true;
        core.Movement.SetVelocityZero();

        swingPos = DetermineSwingObjectPosition();

        startPos.Set(swingPos.x, swingPos.y -(player.MovementCollider.height / 2), 0f);
        player.AnimateCameraChannel.RaiseEvent(core.Movement.FacingDirection,0);
        player.transform.position = startPos;
    }

    public override void Exit()
    {
        base.Exit();
        player.RB.isKinematic = false;
        player.transform.rotation= originalPlayerRotation;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        xInput = player.InputHandler.NormInputX;
        jumpInput = player.InputHandler.JumpInput;

        if (jumpInput)
        {
            stateMachine.ChangeState(player.JumpState);
        }
        else
        {
            if(!isExitingState)
            {
                core.Movement.SetVelocityZero();
                player.transform.position = startPos;
            }
        }
    }

    public void SetDetectedPosition(Vector3 pos) => detectedPos = pos;

    private Vector3 DetermineSwingObjectPosition()
    {
        Physics.Raycast(core.CollisionSenses.LedgeCheckHorizontal.position, Vector3.right * core.Movement.FacingDirection, out RaycastHit hit, core.CollisionSenses.WallCheckDistance, core.CollisionSenses.WhatIsSwing);
        CapsuleCollider collider = (CapsuleCollider)hit.collider;
        float xDist = hit.distance;
        workspace.Set(hit.collider.transform.position.x, hit.collider.transform.position.y, 0f);
        return workspace;
    }
}