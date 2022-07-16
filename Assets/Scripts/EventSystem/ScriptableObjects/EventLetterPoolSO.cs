using UnityEngine;
using UOP1.Factory;
using UOP1.Pool;

[CreateAssetMenu(fileName = "NewEventLetterPool", menuName = "Pool/EventLetterPool")]
public class EventLetterPoolSO : ComponentPoolSO<UIEventTile>
{
    [SerializeField] private EventLetterFactorySO _factory;

    public override IFactory<UIEventTile> Factory
    {
        get
        {
            return _factory;
        }
        set
        {
            _factory = value as EventLetterFactorySO;
        }
    }
}
