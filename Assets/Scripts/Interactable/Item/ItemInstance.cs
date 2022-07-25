using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(SphereCollider))]
public class ItemInstance : MonoBehaviour
{
    public Rigidbody RB => _rb;
    public ItemSO Item => _item;

    private Rigidbody _rb;
    private SpriteRenderer _renderer;
    private SphereCollider _collider;
    private readonly Stack<Collider> _ignoredColliders = new Stack<Collider>();

    [SerializeField] private ItemSO _item = default;
    [SerializeField] private LayerMask _ignoreCollisionWhenThrownForDrop;
    [SerializeField] private LayerMask _playerCollisionLayer;

    [SerializeField]
    [Header("Broadcasting on")]
    private ItemEventChannelSO _itemLootChannelSO = default;

    private void Awake()
    {
        _rb = this.GetComponent<Rigidbody>();
        _renderer = this.GetComponent<SpriteRenderer>();
        _collider = this.GetComponent<SphereCollider>();
    }

    private void Start()
    {

    }

    private void Update()
    {
        if (_rb.velocity.y < 0f)
        {
            while (_ignoredColliders.Count > 0)
            {
                Collider ignoredCollider = _ignoredColliders.Pop();
                Physics.IgnoreCollision(_collider, ignoredCollider, false);
            }
        }
    }

    public void SetItem(ItemSO item)
    {
        _item = item;
        _renderer = this.GetComponent<SpriteRenderer>();
        _renderer.sprite = item.PreviewImage;
    }

    public void OnCollisionEnter(Collision collision)
    {
        //If item collides with player pick it up!
        if(_playerCollisionLayer == (_playerCollisionLayer | (1 << collision.gameObject.layer)))
        {
            //This avoid flickering with animations due to collision
            this.gameObject.SetActive(false);

            //Notify loot
            _itemLootChannelSO?.RaiseEvent(this.Item);

            //We should return object to pool not destroy it!
            Destroy(this.gameObject);
        }
        else if (_ignoreCollisionWhenThrownForDrop == (_ignoreCollisionWhenThrownForDrop | (1 << collision.gameObject.layer)))
        {
            _ignoredColliders.Push(collision.collider);
            Physics.IgnoreCollision(_collider, collision.collider, true);
        }
    }
}