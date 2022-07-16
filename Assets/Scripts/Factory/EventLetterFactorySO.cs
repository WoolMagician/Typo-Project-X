using UnityEngine;
using UOP1.Factory;

[CreateAssetMenu(fileName = "NewEventLetterFactory", menuName = "Factory/EventLetterFactory")]
public class EventLetterFactorySO : FactorySO<UIEventTile>
{
    public UIEventTile prefab = default;

    public override UIEventTile Create()
    {
        return Instantiate(prefab);
    }
}