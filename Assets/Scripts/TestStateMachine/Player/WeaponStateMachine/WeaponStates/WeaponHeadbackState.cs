using UnityEngine;
using System.Collections;

public class WeaponHeadbackState : WeaponState
{
    private int _timesPassedByPlayer = 0;
    private bool _canIncPassedByPlayer = false;
    private const int PASS_BY_PLAYER_MAX_COUNT = 1;

    public WeaponHeadbackState(Weapon weapon) : base(weapon)
    {
    }

    public override void Enter()
    {
        base.Enter();

        _canIncPassedByPlayer = false;

        //Detach connected body to allow free comeback without
        // the need of using joint
        weapon.WeaponJoint.connectedBody = null;

        //Update free rotation
        weapon.WeaponHead.Rotate(weapon.WeaponSO.RotateHeadOnThrow);

        //Simulate small height change on headback start
        // this is due to quick stop of weapon
        weapon.WeaponHead.Rigidbody.AddForce(Vector3.up, ForceMode.Impulse);
    }

    public override void Exit()
    {
        base.Exit();
        _timesPassedByPlayer = 0;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        //Set animation state
        if (this.headDistanceFromHeadbackPosition < 0.5f)
        {
            weapon.Player.Anim.SetFloat("weaponThrowState", 2);
        }

        //Return if we are about to exit the state
        if (isExitingState) return;

        //This checks if the head is coming back to the joint position
        // If true, it will always mean that we have to catch it back
        //Filtering with the passby parameter will give a bounce back effect before catch!
        if (this.IsHeadMovingTowardsJoint())
        {
            //If we need to increment passby then JUST.....DO - IT!
            if (_canIncPassedByPlayer)
            {
                _timesPassedByPlayer += 1;
                _canIncPassedByPlayer = false;
            }
        }
        else
        {
            _canIncPassedByPlayer = true;
        }

        //If the distance is less then threshold and the weapon
        // passed by the player the given amount of times then
        // reset the throw state and catch the weapon
        if (this.CanCatch())
        {
            //We slightly delay the exit to allow better graphical result
            weapon.StartCoroutine(_DelayedExit());
        }
        else
        {
            //Keep on heading back to the headback position

            //Get normalized velocity vector used for headback
            Vector3 newHeadVelocity = (headbackPosition - weapon.WeaponHead.transform.position).normalized;

            //Multiply for the headback speed of the weapon
            newHeadVelocity *= weapon.WeaponSO.HeadbackSpeed;

            //Add force to head, we are not using directly the velocity property
            // because we want the head to be a little "sloppy"
            weapon.WeaponHead.Rigidbody.velocity = newHeadVelocity;
        }

    }

    private bool CanCatch()
    {
        return this.headDistanceFromHeadbackPosition < weapon.HeadbackCatchThreshold && 
               _timesPassedByPlayer >= PASS_BY_PLAYER_MAX_COUNT;
    }

    private IEnumerator _DelayedExit()
    {
        //Set exiting to stop headback process
        isExitingState = true;

        //Decrease significantly speed
        weapon.WeaponHead.Rigidbody.velocity *= 0.1f;        
        
        yield return new WaitForSeconds(0.1f);

        //If player kept the input pressed and we are grounded move to charging
        if (weapon.Player.InputHandler.AttackInput && weapon.Player.Core.CollisionSenses.IsGrounded())
        {
            stateMachine.ChangeState(weapon.ChargeState);
        }
        else
        {
            stateMachine.ChangeState(weapon.IdleState);
        }
    }
}