using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "NewChest", menuName = "Interactables/Chest")]
public class ChestSO : SerializableScriptableObject
{
    [SerializeField]
    private bool _isOpened = false;

    [SerializeField]
    private ChestTypeSO _chestTypeSO = default;

    [SerializeField]
    public ItemSO _containedItemSO;

    public ChestTypeSO ChestTypeSO => _chestTypeSO;
    public ItemSO ContainedItemSO => _containedItemSO;

    public UnityAction<bool> OnOpenedChange;

    public void SetOpened(bool value)
    {
        if (_isOpened != value)
        {
            _isOpened = value;
            this.OnOpenedChange.Invoke(value);
        }
    }

    public bool GetOpened()
    {
        return _isOpened;
    }

    public Vector3 GetColliderSize(bool isometric)
    {
        return this._isOpened ?
               (isometric ? this.ChestTypeSO.IsometricOpenedChestData.ColliderSize : this.ChestTypeSO.OpenedChestData.ColliderSize) :
               (isometric ? this.ChestTypeSO.IsometricClosedChestData.ColliderSize : this.ChestTypeSO.ClosedChestData.ColliderSize);
    }

    public Sprite GetChestSprite(bool isometric)
    {
        return this._isOpened ? 
               (isometric ? this.ChestTypeSO.IsometricOpenedChestData.Sprite : this.ChestTypeSO.OpenedChestData.Sprite) :
               (isometric ? this.ChestTypeSO.IsometricClosedChestData.Sprite : this.ChestTypeSO.ClosedChestData.Sprite);
    }    
}