using UnityEngine;
using UOP1.Factory;

[CreateAssetMenu(fileName = "NewItemFactory", menuName = "Factory/Item Factory")]
public class ItemFactorySO : FactorySO<ItemInstance>
{
    public ItemInstance prefab = default;

    public override ItemInstance Create()
    {
        return Instantiate(prefab);
    }
}