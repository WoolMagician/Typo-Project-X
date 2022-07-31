using System;
using UnityEngine;

[Serializable]
public enum ZSwapType
{
    Upwards = 1,
    Downwards = -1
}

[RequireComponent(typeof(BoxCollider))]
public class ZSwapTrigger : MonoBehaviour
{
    private BoxCollider _collider;
    [SerializeField] private ZSwapType _zSwapType;

    public ZSwapType ZSwapType { get { return _zSwapType; } }

    private void Start()
    {
        _collider = this.GetComponent<BoxCollider>();
    }
}