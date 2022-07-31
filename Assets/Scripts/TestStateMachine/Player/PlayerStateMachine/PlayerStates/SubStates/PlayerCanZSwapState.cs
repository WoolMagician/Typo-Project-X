using UnityEngine;

/// <summary>
/// State used to prepare for ZSwapping
/// This is still a grounded state as ZSwapping can't happen in-air
/// </summary>
public class PlayerCanZSwapState : PlayerGroundedState
{
    protected int _zSwapDirection;

    //Stores enter state player position,
    // this will be used to keep player position later
    private Vector3 startPos;

    public PlayerCanZSwapState(Player player, string animBoolName) : base(player, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        startPos = player.transform.position;
        player.Core.Movement.SetVelocityZero();
        player.AnimateCameraChannel.RaiseEvent(0, -_zSwapDirection);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        //Update animator swap direction
        player.Anim.SetInteger("zSwapDirection", _zSwapDirection);

        if (isExitingState) return;

        //If player releases Y input, swap to move or idle state accordingly
        if(yInput != _zSwapDirection)
        {
            player.StateMachine.ChangeState(player.IdleState);
        }
        else if(xInput != 0)
        {
            player.StateMachine.ChangeState(player.MoveState);
        }
        else if(JumpInput)
        {
            //Perform ZSwapping
            player.IsZSwapState.SetZSwapDirection(_zSwapDirection);
            player.StateMachine.ChangeState(player.IsZSwapState);
        }
        else
        {
            //Lock player in position
            player.transform.position = startPos;
            player.Core.Movement.SetVelocityZero();
        }
    }

    public void SetZSwapDirection(int direction)
    {
        _zSwapDirection = direction;
    }
}