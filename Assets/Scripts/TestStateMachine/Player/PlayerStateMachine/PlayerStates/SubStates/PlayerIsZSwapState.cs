using UnityEngine;

enum ZSwapStatus
{
    Falling = -1,
    Undefined = 0,
    Jumping,
    WallGrab,
    Climbing,
    Ending,
}

public class PlayerIsZSwapState : PlayerAbilityState
{
    private Vector3 wallPosition;
    private Vector3 lockPos;
    private Vector3 cornerPos;
    private Vector3 startPos;
    private Vector3 stopPos;
    private Vector3 workspace;

    protected int _zSwapDirection;
    private ZSwapStatus _status;

    public PlayerIsZSwapState(Player player, string animBoolName) : base(player, animBoolName)
    {
    }

    public override void AnimationFinishTrigger()
    {
        base.AnimationFinishTrigger();

        //If we are in wallgrab, go forward to climbing
        if (_status == ZSwapStatus.WallGrab)
        {
            _status = ZSwapStatus.Climbing;
        }
        //If we are in ending state and animation finish triggers then we are done
        else if(_status == ZSwapStatus.Ending)
        {
            isAbilityDone = true;
        }
    }

    public override void Enter()
    {
        base.Enter();

        //Make sure to reset player speed
        core.Movement.SetVelocityZero();

        //Get starting position
        startPos = player.transform.position;

        //Get wall position
        wallPosition = DetermineWallPosition() + (Vector3.up * 0.35f);

        //This could be avoided because we set the event already in the previous state
        player.AnimateCameraChannel.RaiseEvent(0, -_zSwapDirection);

        //Set initial status to jumping
        _status = ZSwapStatus.Jumping;
    }

    public override void Exit()
    {
        base.Exit();

        //This is just for testing purposes!!!
        // if not enought space, return to start Z position so that the player will just fall
        //player.transform.position = new Vector3(player.transform.position.x, player.transform.position.y, startPos.z);

        //Set player final position
        player.transform.position = stopPos;

        //Reset status and animator
        _status = ZSwapStatus.Undefined;
        player.Anim.SetInteger("zSwapStatus", (int)_status);

        //Reset swap direction on animator
        player.Anim.SetInteger("zSwapDirection", 0);
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        //Avoid to perform operations while exiting state
        if (isExitingState) return;

        //Update animator
        player.Anim.SetInteger("zSwapStatus", (int)_status);

        //Lock player velocity, position will be handled manually
        if(_status != ZSwapStatus.Falling) core.Movement.SetVelocityZero();

        switch (_status)
        {
            case ZSwapStatus.Falling:
                if(core.CollisionSenses.IsGrounded())
                {
                    _status = ZSwapStatus.Ending;
                    lockPos = player.transform.position;
                    stopPos = lockPos;
                }
                break;

            case ZSwapStatus.Jumping:
                if (_zSwapDirection == 1)
                {
                    //Simulate jump up by interpolating position
                    player.transform.position = Vector3.Slerp(player.transform.position, wallPosition, 4f * Time.deltaTime);

                    if (Mathf.Abs((player.transform.position - wallPosition).magnitude) < 0.1f)
                    {
                        _status = ZSwapStatus.WallGrab;
                    }
                }
                else
                {
                    //Simulate jump down by interpolating position
                    Vector3 newPosition = new Vector3(player.transform.position.x, startPos.y + 0.5f, startPos.z - 0.5f);
                    player.transform.position = Vector3.Lerp(player.transform.position, newPosition, 5f * Time.deltaTime);

                    if (Mathf.Abs((player.transform.position - newPosition).magnitude) < 0.1f)
                    {
                        _status = ZSwapStatus.Falling;
                    }
                }
                break;
            case ZSwapStatus.Climbing:
                //Go upwards until we reach end
                player.transform.position = Vector3.Lerp(player.transform.position, player.transform.position + Vector3.up, 0.9f * Time.deltaTime);

                //If we are climbing and we can end the swap then just end it
                if(this.CanEndSwap())
                {
                    _status = ZSwapStatus.Ending;
                    lockPos = player.transform.position;

                    //Determine corner position
                    // TODO:Maybe later on we will have to determine if we have something overhead and stop the swap
                    cornerPos = DetermineCornerPosition();
                    stopPos.Set(cornerPos.x, cornerPos.y + playerData.stopOffset.y, cornerPos.z + 0.5f);
                    stopPos = GetUpdatedStopPosition();
                }
                break;
            case ZSwapStatus.Ending:
                //Keep transform locked while ending anim is playing
                player.transform.position = lockPos;
                break;
            default:
                break;
        }
    }

    public void SetZSwapDirection(int direction)
    {
        _zSwapDirection = direction;
    }

    private Vector3 DetermineWallPosition()
    {
        Physics.Raycast(core.CollisionSenses.WallCheck.position, Vector3.forward, out RaycastHit xHit, 1f, core.CollisionSenses.WhatIsGround);
        return new Vector3(xHit.point.x, xHit.point.y, xHit.point.z - player.MovementCollider.radius);
    }

    private bool CanEndSwap()
    {
        return !Physics.Raycast(core.CollisionSenses.LedgeCheckHorizontal.position, Vector3.forward, 1f, core.CollisionSenses.WhatIsGround);
    }

    private Vector3 GetUpdatedStopPosition()
    {
        Physics.Raycast(stopPos, Vector3.down, out RaycastHit xHit, core.CollisionSenses.WhatIsGround);
        return xHit.point + new Vector3(0, 0, 0);
    }

    private Vector3 DetermineCornerPosition()
    {
        Physics.Raycast(core.CollisionSenses.WallCheck.position, Vector3.forward, out RaycastHit zHit, 1f, core.CollisionSenses.WhatIsGround);
        float zDist = zHit.distance;
        workspace.Set((zDist + 0.015f), 0f, 0f);
        Physics.Raycast(core.CollisionSenses.LedgeCheckHorizontal.position + workspace, Vector3.down, out RaycastHit yHit, core.CollisionSenses.LedgeCheckHorizontal.position.y - core.CollisionSenses.WallCheck.position.y + 0.015f, core.CollisionSenses.WhatIsGround);
        float yDist = yHit.distance;

        workspace.Set(core.CollisionSenses.WallCheck.position.x, core.CollisionSenses.LedgeCheckHorizontal.position.y - yDist, core.CollisionSenses.WallCheck.position.z * zDist);
        return workspace;
    }

}