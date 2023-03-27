using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIInventory : MonoBehaviour
{
    public InventoryWithSlots inventory => _tester.inventory;

    [SerializeField] private InventoryItemInfo _appleInfo; 
    [SerializeField] private InventoryItemInfo _pepperInfo;

    private UITester _tester;

    private void Start()
    {
        var uiSlots = GetComponentsInChildren<UIInventorySlot>();
        _tester = new UITester(_appleInfo, _pepperInfo, uiSlots);
        _tester.OnFillingSlots();
    }
}
