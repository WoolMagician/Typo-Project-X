using UnityEngine;

public class PlayerThrowWeaponState : PlayerAbilityState
{
    private int xInput;
    private float yInput;
    private float yInputRaw;
    private bool attackInput;
    private bool attackInputStop;
    private bool thrown;

    public const string WEAPON_THROW_STATE_ANIM_FLOAT_NAME = "weaponThrowState";

    public PlayerThrowWeaponState(Player player, string animBoolName) : base(player, animBoolName)
    {
    }

    public override void Enter()
    {
        //Call enter after having set the throw type
        base.Enter();

        //Store player position
        // to be used with wall and ground throw
        //startPos = player.transform.position;

        player.weaponController.OnThrowEnd += HandleThrowEnd;
    }

    private void HandleThrowEnd()
    {
        isAbilityDone = true;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        //Avoid to execute stuff while is exiting
        if (isExitingState) return;

        attackInputStop = player.InputHandler.AttackInputStop;
        attackInput = player.InputHandler.AttackInput;

        if (!attackInputStop && attackInput)
        {
            //Execute weapon throw
            if (player.weaponController.StateMachine.CurrentState != player.weaponController.ThrowState && !thrown)
            {
                thrown = true;
                yInput = player.InputHandler.NormInputY;
                xInput = player.InputHandler.NormInputX;
                yInputRaw = player.InputHandler.RawMovementInput.y;

                player.Anim.SetFloat("yInput", yInputRaw);
                player.weaponController.ThrowState.SetThrowingDirectionAndCharge(new Vector3(xInput, yInput, 0f), 0);
                player.weaponController.StateMachine.ChangeState(player.weaponController.ThrowState);
            }
        }
    }

    public override void Exit()
    {
        base.Exit();
        thrown = false;
        player.weaponController.OnThrowEnd -= HandleThrowEnd;
        player.Anim.SetFloat(WEAPON_THROW_STATE_ANIM_FLOAT_NAME, -1);
    }
}