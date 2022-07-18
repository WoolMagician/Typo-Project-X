using UnityEngine;

public class PlayerMoveState : PlayerGroundedState
{
    private int mLastFrameXInput;
    private float interpolatedMovementVelocity;

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
        this.mLastFrameXInput = core.Movement.FacingDirection;
        this.interpolatedMovementVelocity = Mathf.Abs(core.Movement.CurrentVelocity.x);
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

        //If we are not already transitioning to something else and we are standing still go to idle state.
        if (!isExitingState)
        {
            if (xInput == 0)
            {
                //Interpolate velocity to simulate friction
                interpolatedMovementVelocity = Mathf.Lerp(interpolatedMovementVelocity, 0, playerData.friction * Time.deltaTime) * core.Movement.FacingDirection;

                //Set only X velocity
                core.Movement.SetVelocityX(interpolatedMovementVelocity);

                if (Mathf.Abs(core.Movement.CurrentVelocity.x) < 0.01)
                {
                    stateMachine.ChangeState(player.IdleState);
                }
            }
            else
            {
                //Player snapped input to opposite direction, play change direction
                if (this.mLastFrameXInput != 0 && 
                    Mathf.Sign(this.mLastFrameXInput) != xInput && 
                    Mathf.Abs(player.InputHandler.RawMovementInput.x) > 0.5f)
                {
                    stateMachine.ChangeState(player.MoveDirectionChangeState);
                }
                else
                {
                    //Store last input to execute direction change
                    this.mLastFrameXInput = xInput;

                    //Adjust movement vector to ground slope
                    Vector3 movementVector = core.Movement.AdjustMotionVectorToGroundSlope(core.CollisionSenses.groundNormal, 
                                                                                           new Vector3(xInput, core.Movement.CurrentVelocity.y, 0));

                    //Interpolate velocity to simulate acceleration
                    interpolatedMovementVelocity = Mathf.Lerp(interpolatedMovementVelocity,
                                                              playerData.movementVelocity * (player.InputHandler.DashInput ? playerData.animalDashVelocityMultiplier : 1f), 
                                                              playerData.acceleration * Time.deltaTime);

                    //Set motion vector
                    core.Movement.SetVelocity(interpolatedMovementVelocity, movementVector, 1);
                }
            }
        }
    }
}