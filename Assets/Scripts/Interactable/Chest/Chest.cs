using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class Chest : MonoBehaviour, IGrabbable
{
    private SpriteRenderer _spriteRenderer;
    private BoxCollider _collider;

    [SerializeField] private ChestSO _chestSO;

    [Header("Broadcasting on")]
    [SerializeField] private ItemDropEventChannelSO _requireItemDropChannelSO = default;

    void Start()
    {
        this._spriteRenderer = this.GetComponent<SpriteRenderer>();
        this._collider = this.GetComponent<BoxCollider>();
        this.UpdateChest();
    }

    private void OnEnable()
    {
        this._chestSO.OnOpenedChange += HandleChestOpenedChange;
    }

    private void OnDisable()
    {
        this._chestSO.OnOpenedChange -= HandleChestOpenedChange;
    }

    private void HandleChestOpenedChange(bool value)
    {
        if (value && _chestSO.ContainedItemSO != null)
        {
            _requireItemDropChannelSO?.RaiseEvent(_chestSO.ContainedItemSO, this.transform.position, Vector3.up * 3f);
        }
        this.UpdateChest();
    }

    private void UpdateChest()
    {
        this.UpdateChestSprite();
        this.UpdateChestCollider();
    }

    private void UpdateChestSprite()
    {
        this._spriteRenderer.sprite = _chestSO.GetChestSprite(false);
    }

    private void UpdateChestCollider()
    {
        Vector3 size = _chestSO.GetColliderSize(false);
        Vector3 center = _collider.center;
        Vector3 workspace = Vector3.zero;

        workspace.Set(size.x, size.y, size.z);

        center.x += (size.x - _collider.size.x) / 2;
        center.y += (size.y - _collider.size.y) / 2;
        center.z += (size.z - _collider.size.z) / 2;

        _collider.size = workspace;
        _collider.center = center;
    }

    #region GRAB BEHAVIOUR

    public bool CanGrab()
    {
        //Grab only if chest is closed
        return !_chestSO.GetOpened();
    }

    public void EnterGrabState()
    {
        //Play particles ???
    }

    public void ExitGrabState()
    {
        //On exit set chest opened
        //To add inventory check for old man keys
        _chestSO.SetOpened(true);
    }

    #endregion

}