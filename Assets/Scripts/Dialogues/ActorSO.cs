using UnityEngine;
using UnityEngine.Localization;

public enum ActorID
{
    OldWiseMan1K,
    OldWiseMan1M,
    OldWiseMan10K,
    OldWiseMan100,
    Baron,
    Charles,
    Fisherman,
    Mermaid,
    Mizuno,
    MotocrossMan,
    SafariMan,
    Tomba,
    WorldGreatestThief,
    Yan
}

/// <summary>
/// Scriptable Object that represents an "Actor", that is the protagonist of a Dialogue
/// </summary>
[CreateAssetMenu(fileName = "newActor", menuName = "Dialogues/Actor")]
public class ActorSO : ScriptableObject
{
    [SerializeField] private ActorID _actorId = default;
    [SerializeField] private LocalizedString _actorName = default;

    public ActorID ActorId { get => _actorId; }
    public LocalizedString ActorName { get => _actorName; }
}
