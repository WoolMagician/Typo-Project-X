using UnityEngine;

public class WeaponThrowState : WeaponState
{
    private Vector3 _throwingDirection;

    public WeaponThrowState(Weapon weapon) : base(weapon)
    {
    }

    public override void Enter()
    {
        base.Enter();

        //Configure joint for throwing
        weapon.WeaponJoint.gameObject.SetActive(true);
        weapon.WeaponJoint.connectedBody = weapon.WeaponHead.Rigidbody;
        weapon.WeaponJoint.anchor = Vector3.zero;
        weapon.WeaponRopeRenderer.enabled = true;

        weapon.WeaponHead.transform.position = weapon.WeaponJoint.transform.position;
        weapon.WeaponHead.gameObject.SetActive(true);

        //Make sure to reset any accumulated velocity
        // before throwing again
        weapon.WeaponHead.Rigidbody.velocity = Vector3.zero;
        weapon.WeaponHead.transform.rotation = Quaternion.identity;

        //Apply actual throw force
        weapon.WeaponHead.Rigidbody.AddForce(weapon.WeaponSO.ThrowSpeed * this.GetUpdatedThrowDirection(_throwingDirection), ForceMode.Impulse);

        //Set player animator
        weapon.Player.Anim.SetFloat("weaponThrowState", 1);

        //Update free rotation
        this.weapon.WeaponHead.Rotate(weapon.WeaponSO.RotateHeadOnThrow);

        //Update yInput for animations only while charging
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (isExitingState) return;

        //Make sure to reset anchor and connected body to the one we want
        // this will also ensure the joint to work properly by updating values
        weapon.WeaponJoint.connectedBody = weapon.WeaponHead.Rigidbody;
        weapon.WeaponJoint.anchor = Vector3.zero;

        //This checks if the head is coming back to the joint position
        // If true, it will always mean that we have to catch it back
        if (IsHeadMovingTowardsJoint())
        {
            //Change to headback state
            stateMachine.ChangeState(weapon.HeadbackState);
        }
    }

    public void SetThrowingDirectionAndCharge(Vector3 direction, int charge)
    {
        _throwingDirection = direction;
    }

    private Vector3 GetUpdatedThrowDirection(Vector3 direction)
    {
        float xDirection;

        //If movement input is zero then use facing direction
        if (direction.x == 0 && direction.y == 0)
        {
            xDirection = weapon.Player.Core.Movement.FacingDirection;
        }
        else
        {
            //Take the normalized X input
            xDirection = direction.x;
        }
        return new Vector3(xDirection,
                           direction.y,
                           0f).normalized;
    }
}