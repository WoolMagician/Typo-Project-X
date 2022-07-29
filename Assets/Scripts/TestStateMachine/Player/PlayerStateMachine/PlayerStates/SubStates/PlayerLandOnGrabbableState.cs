using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLandOnGrabbableState : PlayerGroundedState
{
    private IGrabbable _grabbedObject;

    public PlayerLandOnGrabbableState(Player player, string animBoolName) : base(player, animBoolName)
    {
    }

    public override void Enter()
    {
        player.Anim.SetInteger("landType", 1);
        base.Enter();
        core.Movement.SetVelocityZero();
        player.Anim.SetFloat("yVelocity", 0);
        player.Anim.SetFloat("xVelocity", 0);
        _grabbedObject?.EnterGrabState();
    }

    public override void LogicUpdate()
    {
        xInput = player.InputHandler.NormInputX;
        JumpInput = player.InputHandler.JumpInput;
        core.Movement.CheckIfShouldFlip(xInput);

        if (!isExitingState)
        {
            if (JumpInput)
            {
                stateMachine.ChangeState(player.JumpState);
                _grabbedObject?.ExitGrabState();
            }
            else
            {
                MonoBehaviour mono = ((MonoBehaviour)_grabbedObject);

                core.Movement.SetVelocityZero();
                player.transform.position = new Vector3(mono.transform.position.x, player.transform.position.y, player.transform.position.z);
            }
        }
    }

    public void SetGrabbedObject(IGrabbable grabbedObj)
    {
        this._grabbedObject = grabbedObj;
    }
}