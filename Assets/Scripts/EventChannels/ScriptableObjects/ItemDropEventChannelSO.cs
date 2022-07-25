using UnityEngine.Events;
using UnityEngine;

/// <summary>
/// This class is used for Item interaction events.
/// Example: Pick up an item passed as paramater
/// </summary>

[CreateAssetMenu(menuName = "Events/Item Drop Event Channel")]
public class ItemDropEventChannelSO : DescriptionBaseSO
{
    public UnityAction<ItemSO, Vector3, Vector3> OnEventRaised;

    public void RaiseEvent(ItemSO item, Vector3 position, Vector3 velocityOnSpawn)
    {
        if (OnEventRaised != null)
            OnEventRaised.Invoke(item, position, velocityOnSpawn);
    }
}