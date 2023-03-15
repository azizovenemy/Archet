using System;

public interface IInventory
{
    int capacity { get; set; }
    bool isFull { get; }

    int GetItemAmount(Type itemType);
    IInventoryItem GetItem(Type itemType);
    IInventoryItem[] GetAllItems();
    IInventoryItem[] GetAllItems(Type itemType);
    IInventoryItem[] GetEquippedItems();

    bool TryToAdd(object sender, IInventoryItem item);
    void Remove(object sender, Type itemType, int amount = 1);
    bool HasItem(Type itemType, out IInventoryItem item);
}
