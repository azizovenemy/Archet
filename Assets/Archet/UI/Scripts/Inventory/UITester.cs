using System.Collections.Generic;
using UnityEngine;

public class UITester
{
    private List<InventoryItemInfo> inventoryItems;
    private UIInventorySlot[] _uiSlots;

    public InventoryWithSlots inventory { get; }

    public UITester(List<InventoryItemInfo> inventoryItems, UIInventorySlot[] uiSlots)
    {
        this.inventoryItems = inventoryItems;
        _uiSlots = uiSlots;

        inventory = new InventoryWithSlots(uiSlots.Length);
        inventory.OnInventoryStateChangedEvent += OnInventoryStateChanged;
    }

    public void OnFillingSlots()
    {
        var allSlots = inventory.GetAllSlots();
        var avaliableSlots = new List<IInventorySlot>(allSlots);
        int filledSlots = 15;

        for (int i = 0; i < filledSlots; i++)
        {
            var filledSlot = AddRandomItemIntoRandomSlot(avaliableSlots);
            avaliableSlots.Remove(filledSlot);
        }

        SetupInventoryUI(inventory);
    }

    private IInventorySlot AddRandomItemIntoRandomSlot(List<IInventorySlot> slots)
    {
        var rSlotIndex = Random.Range(0, slots.Count);
        var rSlot = slots[rSlotIndex];
        var rCount = Random.Range(1, 4);
        var itemIndex = Random.Range(0, inventoryItems.Count);
        var apple = new Item(inventoryItems[itemIndex]);
        apple.state.amount = rCount;
        inventory.TryToAddToSlot(this, rSlot, apple);
  
        return rSlot;
    }

    private void SetupInventoryUI(InventoryWithSlots inventory)
    {
        var allSlots = inventory.GetAllSlots();
        var allSlotsLength = allSlots.Length;
        for (int i = 0; i < allSlotsLength; i++)
        {
            var slot = allSlots[i];
            var uiSlot = _uiSlots[i];
            uiSlot.SetSlot(slot);
            uiSlot.Refresh();
        }
    }

    private void OnInventoryStateChanged(object sender)
    {
        foreach(var uiSlot in _uiSlots)
        {
            uiSlot.Refresh();
        }
    }
}
