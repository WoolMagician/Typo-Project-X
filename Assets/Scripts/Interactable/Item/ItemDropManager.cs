using UnityEngine;

public class ItemDropManager : MonoBehaviour
{
    [SerializeField]
    private ItemPoolSO _pool = default;

    [SerializeField] 
    [Header("Listening on")]
    private ItemDropEventChannelSO _requireItemDropChannelSO = default;

    private void OnEnable()
    {
        _requireItemDropChannelSO.OnEventRaised += HandleItemDropRequest;
    }

    private void OnDisable()
    {
        _requireItemDropChannelSO.OnEventRaised -= HandleItemDropRequest;
    }

    private void HandleItemDropRequest(ItemSO item, Vector3 position, Vector3 velocityOnSpawn)
    {
       ItemInstance itemInst = _pool.Request();
       itemInst.SetItem(item);
       itemInst.transform.position = position;

       if (itemInst.RB != null)
       {
           itemInst.RB.velocity = velocityOnSpawn;
       }
    }
}