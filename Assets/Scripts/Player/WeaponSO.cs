using UnityEngine;

[CreateAssetMenu(fileName = "NewWeapon", menuName = "Inventory/Equip/Weapon")]
public class WeaponSO : ItemSO
{
    [SerializeField] private Sprite _ropeSprite;
    [SerializeField] private float _throwSpeed;
    [SerializeField] private float _headbackSpeed;
    [SerializeField] private bool _rotateHeadOnThrow;
    [SerializeField] private bool _alwaysFaceThrowingDirection;

    public Sprite RopeSprite { get { return _ropeSprite; } }
    public float ThrowSpeed { get { return _throwSpeed; } }
    public float HeadbackSpeed { get { return _headbackSpeed; } }
    public bool RotateHeadOnThrow { get { return _rotateHeadOnThrow; } }
}