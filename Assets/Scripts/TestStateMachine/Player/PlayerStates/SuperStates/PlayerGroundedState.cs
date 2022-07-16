using UnityEngine;

public class PlayerGroundedState : PlayerState
{
    protected int xInput;
    protected int yInput;

    protected bool isTouchingCeiling;

    private bool JumpInput;
    private bool grabInput;
    private bool isGrounded;
    private bool isTouchingWall;
    private bool isTouchingLedge;
    private bool dashInput;

    public PlayerGroundedState(Player player, string animBoolName) : base(player, animBoolName)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();

        isGrounded = core.CollisionSenses.IsGrounded();
        isTouchingWall = core.CollisionSenses.WallFront;
        isTouchingLedge = core.CollisionSenses.LedgeHorizontal;
    }

    public override void Enter()
    {
        base.Enter();
        player.JumpState.ResetAmountOfJumpsLeft();
        player.AnimateCameraChannel.RaiseEvent(0);
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        xInput = player.InputHandler.NormInputX;
        yInput = player.InputHandler.NormInputY;
        JumpInput = player.InputHandler.JumpInput;
        grabInput = player.InputHandler.GrabInput;
        dashInput = player.InputHandler.DashInput;

        if (JumpInput && player.JumpState.CanJump())
        {
            stateMachine.ChangeState(player.JumpState);
        }
        else if (!isGrounded)
        {
            player.InAirState.StartCoyoteTime();
            stateMachine.ChangeState(player.InAirState);
        }
        else if (isTouchingWall && grabInput && isTouchingLedge)
        {
            stateMachine.ChangeState(player.WallGrabState);
        }
        //else if (dashInput && player.DashState.CheckIfCanDash() && !isTouchingCeiling)
        //{
        //    stateMachine.ChangeState(player.DashState);
        //}
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
