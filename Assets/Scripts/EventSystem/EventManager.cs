using UnityEngine;

public class EventManager : MonoBehaviour
{
    [Header("Listening to")]
    [SerializeField] private EventSystemEventChannelSO _channelEventOccurr = default;
    [SerializeField] private EventSystemEventChannelSO _channelEventClear = default;

    [Header("Broadcasting on")]
    [SerializeField] private EventSystemEventChannelSO _channelUIEventOccurr = default;
    [SerializeField] private EventSystemEventChannelSO _channelUIEventClear = default;

    private void OnEnable()
    {
        _channelEventOccurr.OnEventRaised += HandleEventOccurr;
        _channelEventClear.OnEventRaised += HandleEventClear;
    }

    private void OnDisable()
    {
        _channelEventOccurr.OnEventRaised -= HandleEventOccurr;
        _channelEventClear.OnEventRaised -= HandleEventClear;
    }

    private void HandleEventOccurr(EventDataSO @event)
    {
        @event.SetOccurred();
        this._channelUIEventOccurr.RaiseEvent(@event);
    }

    private void HandleEventClear(EventDataSO @event)
    {
        @event.SetCleared();
        this._channelUIEventClear.RaiseEvent(@event);
    }
}