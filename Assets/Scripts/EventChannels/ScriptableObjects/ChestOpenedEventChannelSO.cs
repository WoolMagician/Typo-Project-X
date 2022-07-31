using UnityEngine.Events;
using UnityEngine;

[CreateAssetMenu(menuName = "Events/Chest Opened Channel")]
public class ChestOpenedEventChannelSO : DescriptionBaseSO
{
    public UnityAction<ChestSO> OnEventRaised;

    public void RaiseEvent(ChestSO chest)
    {
        if (OnEventRaised != null)
            OnEventRaised.Invoke(chest);
    }
}