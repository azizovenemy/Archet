using System;

[Serializable]
public class InventoryItemState : IInventoryItemState
{
    //поля созданы, потому что свойства серилиазовать нельзя, а поля можно
    public bool isItemEquipped;
    public int itemAmount;

    public bool isEquipped { 
        get => isItemEquipped; 
        set => isItemEquipped = value; 
    }

    public int amount { 
        get => itemAmount; 
        set => itemAmount = value; 
    }

    public InventoryItemState()
    {
        itemAmount = 0;
        isItemEquipped = false;
    }
}
