using System.Collections.Generic;
using UnityEngine;

public class UIInventory : MonoBehaviour
{
    public InventoryWithSlots inventory => _tester.inventory;

    [SerializeField] private List<InventoryItemInfo> inventoryItems;

    private UITester _tester;

    private void Start()
    {
        var uiSlots = GetComponentsInChildren<UIInventorySlot>();
        _tester = new UITester(inventoryItems, uiSlots);
        _tester.OnFillingSlots();
    }
}
