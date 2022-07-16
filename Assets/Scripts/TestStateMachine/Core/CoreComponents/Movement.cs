using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : CoreComponent
{
    public Rigidbody RB { get; private set; }
    //public CharacterController CharacterController { get; private set; }

    public int FacingDirection { get; private set; }
    public bool CanSetVelocity { get; set; }

    public Vector3 CurrentVelocity { get; private set; }
    public Vector3 CurrentMovementVector { get; private set; }

    private Vector3 workspace;

    //public MovementSettings movementSettings = new MovementSettings();


    //[Serializable]
    //public class MovementSettings
    //{
    //    [Curve("Air Acceleration Curve (speed over time)", 0, 0, 5f, 1f, true, CurveAttribute.CurveColor.Cyan)]
    //    public AnimationCurve airAccelerationCurve = new AnimationCurve(new Keyframe[] { new Keyframe(0, 0), new Keyframe(.5f, 1) });

    //    [Curve("Air Momentum Curve (speed over time)", 0, 0, 5f, 1f, true, CurveAttribute.CurveColor.Cyan)]
    //    public AnimationCurve airMomentumCurve = new AnimationCurve(new Keyframe[] { new Keyframe(0, 1), new Keyframe(.25f, 0) });

    //    [Curve("Grounded Acceleration Curve (speed over time)", 0, 0, 5f, 1f, true, CurveAttribute.CurveColor.Cyan)]
    //    public AnimationCurve groundedAccelerationCurve = new AnimationCurve(new Keyframe[] { new Keyframe(0, 0), new Keyframe(.5f, 1) });

    //    [Curve("Grounded Deceleration Curve (speed over time)", 0, 0, 5f, 1f, true, CurveAttribute.CurveColor.Cyan)]
    //    public AnimationCurve groundedDecelerationCurve = new AnimationCurve(new Keyframe[] { new Keyframe(0, 1), new Keyframe(.25f, 0) });
    //}



    protected override void Awake()
    {
        base.Awake();

        RB = GetComponentInParent<Rigidbody>(); 
        //CharacterController = GetComponentInParent<CharacterController>();

        FacingDirection = 1;
        CanSetVelocity = true;
    }

    public void LogicUpdate()
    {
        CurrentVelocity = RB.velocity;
    }

    #region Set Functions

    public void SetVelocityZero()
    {
        workspace = Vector3.zero;        
        SetFinalVelocity();
    }

    public void SetVelocity(float velocity, Vector3 angle, int direction)
    {
        angle.Normalize();
        workspace.Set(angle.x * velocity * direction, angle.y * velocity, 0);
        SetFinalVelocity();
    }

    public void SetVelocity(float velocity, Vector3 direction)
    {
        workspace = direction * velocity;
        SetFinalVelocity();
    }

    public void SetVelocityX(float velocity)
    {
        workspace.Set(velocity, CurrentVelocity.y, CurrentVelocity.z);
        SetFinalVelocity();
    }

    public void SetVelocityY(float velocity)
    {
        workspace.Set(CurrentVelocity.x, velocity, CurrentVelocity.z);
        SetFinalVelocity();
    }

    private void SetFinalVelocity()
    {
        if (CanSetVelocity)
        {
            RB.velocity = workspace;
            CurrentVelocity = workspace;
        }
    }

    public void CheckIfShouldFlip(int xInput)
    {
        if (xInput != 0 && xInput != FacingDirection)
        {
            Flip();
        }
    }

    public Vector3 AdjustMotionVectorToGroundSlope(Vector3 groundNormal, Vector3 motionVector)
    {
        float currentSlope = this.GetSlopeAngle(groundNormal);

        var slopeRotation = Quaternion.FromToRotation(this.transform.up, groundNormal);
        var adjustedVelocity = slopeRotation * motionVector;

        Vector3 groundParallel = Vector3.Cross(this.transform.up, groundNormal);
        Vector3 slopeParallel = Vector3.Cross(groundParallel, groundNormal);

        if (currentSlope > 55f) return motionVector + slopeParallel.normalized / 3f;
        if (adjustedVelocity.y < 0) { return adjustedVelocity; }

        return motionVector;

    }

    float GetSlopeAngle(Vector3 normal)
    {
        return Mathf.Round(Vector3.Angle(normal, this.transform.up));
    }

    public void Flip()
    {
        FacingDirection *= -1;
        RB.transform.Rotate(0.0f, 180.0f, 0.0f);
    }

    #endregion
}
