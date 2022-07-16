using UnityEngine;

public class EventTrigger : MonoBehaviour
{
    [SerializeField] private EventDataSO @event;

    [Header("Broadcasting on")]
    [SerializeField] private EventSystemEventChannelSO _channelEvent = default;

    private void OnEnable()
    {
        _channelEvent.RaiseEvent(@event);
        this.enabled=false;
    }
}
