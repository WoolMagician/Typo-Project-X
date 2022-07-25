using UnityEngine;
using UnityEngine.Localization;

[CreateAssetMenu(fileName = "NewEventData", menuName = "Events/EventData")]
public class EventDataSO : SerializableScriptableObject
{
    [SerializeField] private int _id = -1;
    [SerializeField] private LocalizedString _name = default;
    [SerializeField] private LocalizedString _description = default;
    [SerializeField] private int _eventType = -1;
    [SerializeField] private bool _cleared = false;
    [SerializeField] private bool _occurred = false;
    [SerializeField] private int _occurrAPGain = 0;
    [SerializeField] private int _clearAPGain = 0;

    public int ID { get => _id; }
    public bool Occurred { get => _occurred; }
    public bool Cleared { get => _cleared; }

    public LocalizedString Name { get => _name; }
    public LocalizedString Description { get => _description; }

    public int OccurrAPGain { get => _occurrAPGain; }
    public int ClearAPGain { get => _clearAPGain; }

    public void SetOccurred()
    {
        _occurred = true;
    }

    public void SetCleared()
    {
        _cleared = true;
    }

    public void ResetOccurred()
    {
        _occurred = false;
    }

    public void ResetCleared()
    {
        _cleared = false;
    }
}