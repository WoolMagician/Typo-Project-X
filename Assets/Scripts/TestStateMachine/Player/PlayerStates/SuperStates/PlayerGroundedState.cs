using UnityEngine;

public class PlayerGroundedState : PlayerState
{
    protected int xInput;
    protected int yInput;

    protected bool isTouchingCeiling;
    protected float slopeAngle;

    protected bool JumpInput;
    private bool grabInput;
    private bool isGrounded;
    private bool isTouchingWall;
    private bool isTouchingLedge;

    public PlayerGroundedState(Player player, string animBoolName) : base(player, animBoolName)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();

        slopeAngle = core.Movement.GetSlopeAngle(core.CollisionSenses.groundNormal);
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

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        //Update slope angle
        player.Anim.SetFloat("slopeAngle", Mathf.Abs(slopeAngle));

        xInput = player.InputHandler.NormInputX;
        yInput = player.InputHandler.NormInputY;
        JumpInput = player.InputHandler.JumpInput;
        grabInput = player.InputHandler.GrabInput;

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
        else if (xInput == Mathf.Sign(core.Movement.GetSlopeParallel().x) && 
                 slopeAngle >= player.playerData.slopeMaxAngle && 
                 Mathf.Abs(core.Movement.CurrentVelocity.magnitude) > 0.75f)
        {
            stateMachine.ChangeState(player.SlopeSlideState);
        }
    }
}