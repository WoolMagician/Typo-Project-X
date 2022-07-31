using UnityEngine;

public class WeaponState
{
    protected Weapon weapon;
    protected WeaponStateMachine stateMachine;    

    protected bool isExitingState;

    protected float startTime;
    protected float headDistanceFromHeadbackPosition;
    protected Vector3 headbackPosition;

    public WeaponState(Weapon weapon)
    {
        this.weapon = weapon;
        this.stateMachine = weapon.StateMachine;
    }

    public virtual void Enter()
    {
        DoChecks();
        startTime = Time.time;
        isExitingState = false;
    }

    public virtual void Exit()
    {
        isExitingState = true;
    }

    public virtual void LogicUpdate()
    {
        //calculate headback position
        if (this.headDistanceFromHeadbackPosition < 0.5f)
        {
            headbackPosition = weapon.SpriteHandTransform.position - weapon.SpriteHandTransform.right * 0.05f;
        }
        else
        {
            headbackPosition = this.GetHeadbackPosition();
        }

        //Update sprite
        weapon.WeaponHead.SetSprite(weapon.WeaponSO.PreviewImage);

        //Update head distance
        this.headDistanceFromHeadbackPosition = Vector3.Distance(headbackPosition, weapon.WeaponHead.transform.position);
    }

    public virtual void PhysicsUpdate()
    {
        DoChecks();
    }

    public virtual void DoChecks() { }

    protected Vector3 GetHeadbackPosition()
    {
        return weapon.WeaponJoint.transform.position - new Vector3(weapon.WeaponHeadbackOffsetFromJoint.x * weapon.Player.Core.Movement.FacingDirection, 
                                                                   weapon.WeaponHeadbackOffsetFromJoint.y, 0f);
    }

    protected bool IsHeadMovingTowardsJoint()
    {
        Vector3 headToJoint = weapon.WeaponJoint.transform.position - weapon.WeaponHead.transform.position;
        return Vector3.Dot(headToJoint, weapon.WeaponHead.Rigidbody.velocity) > 0;
    }
}