using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Events/Event System Channel")]
public class EventSystemEventChannelSO : DescriptionBaseSO
{
    public UnityAction<EventDataSO> OnEventRaised;

    public void RaiseEvent(EventDataSO ev)
    {
        if (OnEventRaised != null)
            OnEventRaised.Invoke(ev);
    }
}