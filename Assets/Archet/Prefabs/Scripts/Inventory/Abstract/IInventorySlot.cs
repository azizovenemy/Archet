using System;

public interface IInventorySlot
{
    bool isFull { get; }
    bool isEmpty { get; }

    int amount { get; }
    int capacity { get; }
    Type itemType { get; }
    IInventoryItem item { get; }

    void SetItem(IInventoryItem item);
    void Clear();
}
