using System;

public interface IInventoryItem
{
    Type type { get; }
    IInventoryItemInfo info { get; }
    IInventoryItemState state { get; }  

    IInventoryItem Clone();
}
