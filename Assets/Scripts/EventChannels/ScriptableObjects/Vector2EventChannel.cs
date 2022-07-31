using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// This class is used for Events that have one int argument.
/// Example: An Achievement unlock event, where the int is the Achievement ID.
/// </summary>

[CreateAssetMenu(menuName = "Events/Vector2 Event Channel")]
public class Vector2EventChannel : DescriptionBaseSO
{
    public UnityAction<Vector2> OnEventRaised;

    public void RaiseEvent(float x, float y)
    {
        if (OnEventRaised != null)
            OnEventRaised.Invoke(new Vector2(x,y));
    }

    public void RaiseEvent(Vector2 value)
    {
        if (OnEventRaised != null)
            OnEventRaised.Invoke(value);
    }
}