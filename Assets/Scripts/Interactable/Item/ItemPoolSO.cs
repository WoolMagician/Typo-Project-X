using UnityEngine;
using UOP1.Factory;
using UOP1.Pool;

[CreateAssetMenu(fileName = "NewItemPool", menuName = "Pool/Item Pool")]
public class ItemPoolSO : ComponentPoolSO<ItemInstance>
{
    [SerializeField] private ItemFactorySO _factory;

    public override IFactory<ItemInstance> Factory
    {
        get
        {
            return _factory;
        }
        set
        {
            _factory = value as ItemFactorySO;
        }
    }
}