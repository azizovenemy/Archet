using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UITester
{   
    private InventoryItemInfo _appleInfo;
    private InventoryItemInfo _pepperInfo;
    private UIInventorySlot[] _uiSlots;

    public InventoryWithSlots inventory { get; }

    public UITester(InventoryItemInfo appleInfo, InventoryItemInfo pepperInfo, UIInventorySlot[] uiSlots)
    {
        _appleInfo = appleInfo;
        _pepperInfo = pepperInfo;
        _uiSlots = uiSlots;

        inventory = new InventoryWithSlots(uiSlots.Length);
        inventory.OnInventoryStateChangedEvent += OnInventoryStateChanged;
    }

    public void OnFillingSlots()
    {
        var allSlots = inventory.GetAllSlots();
        var avaliableSlots = new List<IInventorySlot>(allSlots);
        int filledSlots = 5;

        for (int i = 0; i < filledSlots; i++)
        {
            var filledSlot = AddRandomAppleIntoRandomSlots(avaliableSlots);
            avaliableSlots.Remove(filledSlot);

            filledSlot = AddRandomPepperIntoRandomSlots(avaliableSlots);
            avaliableSlots.Remove(filledSlot);
        }

        SetupInventoryUI(inventory);
    }

    private IInventorySlot AddRandomAppleIntoRandomSlots(List<IInventorySlot> slots)
    {
        var rSlotIndex = Random.Range(0, slots.Count);
        var rSlot = slots[rSlotIndex];
        var rCount = Random.Range(1, 4);
        var apple = new Apple(_appleInfo);
        apple.state.amount = rCount;
        inventory.TryToAddToSlot(this, rSlot, apple);
  
        return rSlot;
    }

    private IInventorySlot AddRandomPepperIntoRandomSlots(List<IInventorySlot> slots)
    {
        var rSlotIndex = Random.Range(0, slots.Count);
        var rSlot = slots[rSlotIndex];
        var rCount = Random.Range(1, 4);
        var pepper = new Pepper(_pepperInfo);
        pepper.state.amount = rCount;
        inventory.TryToAddToSlot(this, rSlot, pepper);

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
