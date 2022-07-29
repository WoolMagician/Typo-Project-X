using UnityEngine;

public class WeaponChargeState : WeaponState
{
    private const float SPIN_SPEED = 1000f;

    private int xInput;
    private float yInput;
    private float yInputRaw;
    private bool attackInput;
    private bool attackInputStop;

    public WeaponChargeState(Weapon weapon) : base(weapon)
    {
    }

    public override void Enter()
    {
        base.Enter();
        weapon.WeaponHead.Rigidbody.isKinematic = true;
        weapon.WeaponHead.Rigidbody.velocity = Vector3.zero;

        weapon.WeaponRopeRenderer.enabled = true;
        weapon.WeaponHead.gameObject.SetActive(true);

        //Update free rotation
        weapon.WeaponHead.Rotate(false);

        weapon.Player.Anim.SetFloat("weaponThrowState", 0);
    }

    public override void Exit()
    {
        base.Exit();
        weapon.WeaponHead.transform.rotation = Quaternion.identity;
        weapon.WeaponHead.Rigidbody.velocity = Vector3.zero;
        weapon.WeaponHead.Rigidbody.isKinematic = false;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        attackInput = weapon.Player.InputHandler.AttackInput;
        attackInputStop = weapon.Player.InputHandler.AttackInputStop;

        if (isExitingState) return;

        if(attackInput)
        {

            weapon.WeaponHead.transform.position = weapon.SpriteHandTransform.position;

            yInput = weapon.Player.InputHandler.NormInputY;
            xInput = weapon.Player.InputHandler.NormInputX;
            yInputRaw = weapon.Player.InputHandler.RawMovementInput.y;

            weapon.Player.Anim.SetFloat("yInput", yInputRaw);
            weapon.Player.Core.Movement.CheckIfShouldFlip(xInput);
            
            //TODO: Start counting for charge stacks

        }
        else if(attackInputStop)
        {
            this.weapon.ThrowState.SetThrowingDirectionAndCharge(new Vector3(xInput, yInput, 0f), 0);
            this.weapon.StateMachine.ChangeState(this.weapon.ThrowState);
        }
    }
}