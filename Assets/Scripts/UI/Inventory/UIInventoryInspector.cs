using UnityEngine;

public class UIInventoryInspector : MonoBehaviour
{
    [SerializeField] private UIInspectorDescription _inspectorDescription = default;

    public void FillInspector(ItemSO itemToInspect)
    {
        _inspectorDescription.FillDescription(itemToInspect);
    }
}