using UnityEngine;

public class PlayerSlopeSlideState : PlayerGroundedState
{
    private float _originalRigidbodyDrag;
    private bool isSlidingInFacingDirection;

    public PlayerSlopeSlideState(Player player, string animBoolName) : base(player, animBoolName)
    {
    }

    public override void AnimationFinishTrigger()
    {
        base.AnimationFinishTrigger();
        player.Anim.SetBool("slideSlopeEnd", false);
    }

    public override void AnimationTrigger()
    {
        base.AnimationTrigger();
    }

    public override void DoChecks()
    {
        base.DoChecks();
        isSlidingInFacingDirection = core.Movement.GetSlopeAngle() > 0 ? Mathf.Sign(core.Movement.GetSlopeParallel().x) == core.Movement.FacingDirection : true;
    }

    public override void Enter()
    {
        base.Enter();
        _originalRigidbodyDrag = player.RB.drag;
    }

    public override void Exit()
    {
        base.Exit();
        player.RB.drag = _originalRigidbodyDrag;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        //Check where we need to force flip our sprite
        if (!isSlidingInFacingDirection)
        {
            core.Movement.Flip();
        }
        else
        {
            if (slopeAngle < player.playerData.slopeMaxAngle)
            {
                if (!isExitingState)
                {
                    if (isAnimationFinished)
                    {
                        stateMachine.ChangeState(player.IdleState);
                    }
                    else
                    {
                        player.RB.drag = player.playerData.slopeSlideEndDrag;
                        player.Anim.SetBool("slideSlopeEnd", true);
                    }
                }
            }
            else
            {
                player.RB.drag = _originalRigidbodyDrag;
            }
        }
    }
}