using UnityEngine;
using System;

public enum ChestType
{
    _100,
    _1000,
    _10k,
    _1M
}

[Serializable]
public class ChestStatusData
{
    [SerializeField]
    private Sprite _sprite = default;

    [SerializeField]
    private Vector3 _colliderSize = default;

    public Sprite Sprite => _sprite;

    public Vector3 ColliderSize => _colliderSize;
}

[CreateAssetMenu(fileName = "NewChestType", menuName = "Interactables/ChestType")]
public class ChestTypeSO : SerializableScriptableObject
{
    [SerializeField]
    private ChestType _type = default;

    [SerializeField]
    private ChestStatusData _closedChestData = default;

    [SerializeField]
    private ChestStatusData _openedChestData = default;
    
    [SerializeField]
    private ChestStatusData _isoClosedChestData = default;

    [SerializeField]
    private ChestStatusData _isoOpenedChestData = default;

    public ChestType Type => _type;

    public ChestStatusData ClosedChestData => _closedChestData;
    public ChestStatusData OpenedChestData => _openedChestData;
    public ChestStatusData IsometricClosedChestData => _closedChestData;
    public ChestStatusData IsometricOpenedChestData => _openedChestData;
}