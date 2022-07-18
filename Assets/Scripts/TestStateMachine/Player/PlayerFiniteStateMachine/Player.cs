using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Player : MonoBehaviour
{
    #region State Variables
    public PlayerStateMachine StateMachine { get; private set; }

    public PlayerIdleState IdleState { get; private set; }
    public PlayerSlopeSlideState SlopeSlideState { get; private set; }
    public PlayerMoveState MoveState { get; private set; }
    public PlayerMoveDirectionChangeState MoveDirectionChangeState { get; private set; }
    public PlayerJumpState JumpState { get; private set; }
    public PlayerInAirState InAirState { get; private set; }
    public PlayerLandState LandState { get; private set; }
    public PlayerWallSlideState WallSlideState { get; private set; }
    public PlayerWallGrabState WallGrabState { get; private set; }
    public PlayerWallClimbState WallClimbState { get; private set; }
    public PlayerWallJumpState WallJumpState { get; private set; }
    public PlayerLedgeClimbState LedgeClimbState { get; private set; }
    public PlayerSwingGrabState SwingGrabState { get; private set; }

    public PlayerData playerData;
    public IntEventChannelSO AnimateCameraChannel;
    #endregion

    #region Components

    public Core Core { get; private set; }
    public PlayerInputHandler InputHandler { get; private set; }
    public Animator Anim;

    public Rigidbody RB { get; private set; }

    public CapsuleCollider MovementCollider { get; private set; }

    #endregion

    #region Other Variables         

    private Vector3 workspace;
    #endregion

    #region Unity Callback Functions
    private void Awake()
    {
        Core = GetComponentInChildren<Core>();

        StateMachine = new PlayerStateMachine();

        IdleState = new PlayerIdleState(this,"idle");
        SlopeSlideState = new PlayerSlopeSlideState(this, "slopeSlideState");
        MoveState = new PlayerMoveState(this, "move");
        MoveDirectionChangeState = new PlayerMoveDirectionChangeState(this, "move");
        JumpState = new PlayerJumpState(this,"inAir");
        InAirState = new PlayerInAirState(this, "inAir");
        LandState = new PlayerLandState(this, "");
        WallSlideState = new PlayerWallSlideState(this,"wallSlide");
        WallGrabState = new PlayerWallGrabState(this, "wallGrab");
        WallClimbState = new PlayerWallClimbState(this, "wallClimb");
        WallJumpState = new PlayerWallJumpState(this, "inAir");
        LedgeClimbState = new PlayerLedgeClimbState(this, "ledgeClimbState");
        SwingGrabState = new PlayerSwingGrabState(this, "swingGrabState");
    }

    private void Start()
    {
        InputHandler = GetComponent<PlayerInputHandler>();
        RB = GetComponent<Rigidbody>();
        MovementCollider = GetComponent<CapsuleCollider>();
        StateMachine.Initialize(IdleState);
    }

    private void Update()
    {
        Core.LogicUpdate();
        StateMachine.CurrentState.LogicUpdate();
    }

    private void FixedUpdate()
    {
        StateMachine.CurrentState.PhysicsUpdate();
    }
    #endregion

    #region Other Functions

    //public void SetColliderHeight(float height)
    //{
    //    Vector2 center = MovementCollider.offset;
    //    workspace.Set(MovementCollider.size.x, height);

    //    center.y += (height - MovementCollider.size.y) / 2;

    //    MovementCollider.size = workspace;
    //    MovementCollider.offset = center;
    //}

    #endregion

    public void OnDrawGizmos()
    {
        //Gizmos.DrawRay(this.transform.position, Core.Movement.CurrentVelocity);
    }

}