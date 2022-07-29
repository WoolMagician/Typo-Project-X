public class WeaponIdleState : WeaponState
{
    public WeaponIdleState(Weapon weapon) : base(weapon)
    {
    }

    public override void Enter()
    {
        base.Enter();
        
        weapon.WeaponHead.transform.position = weapon.WeaponJoint.transform.position;
        weapon.WeaponJoint.connectedBody = null;
        weapon.WeaponJoint.gameObject.SetActive(false);
        weapon.WeaponHead.gameObject.SetActive(false);
        weapon.WeaponRopeRenderer.enabled = false;
        weapon.Player.weaponController.OnThrowEnd?.Invoke();
    }
}