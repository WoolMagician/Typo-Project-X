using UnityEngine;

public class PlayerSlopeSlideState : PlayerGroundedState
{
    public PlayerSlopeSlideState(Player player, string animBoolName) : base(player, animBoolName)
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

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
        player.Anim.SetBool("slopeSlideState", false);
        player.Anim.SetBool("slideSlopeEnd", false);
        player.RB.drag = 0f;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        float slopeAngle = core.Movement.GetSlopeAngle(core.CollisionSenses.groundNormal);
        player.Anim.SetFloat("slopeAngle", Mathf.Abs(slopeAngle));

        //Check where we need to force flip our sprite
        if (Mathf.Sign(core.Movement.GetSlopeParallel(core.CollisionSenses.groundNormal).x) != core.Movement.FacingDirection)
        {
            core.Movement.Flip();
        }

        //Slope slide with dampening
        if (xInput == 0 && !JumpInput)
        {
            if (slopeAngle > 5f)
            {

            }
            else
            {
                if(!isExitingState)
                {
                    if (isAnimationFinished)
                    {
                        stateMachine.ChangeState(player.IdleState);
                    }
                    else
                    {
                        player.RB.drag = 10f;
                        player.Anim.SetBool("slideSlopeEnd", true);
                    }
                }
            }
        }
        else
        {
            player.StateMachine.ChangeState(player.MoveState);
        }
    }
}
