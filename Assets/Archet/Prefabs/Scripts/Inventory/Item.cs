using System;

public class Item : IInventoryItem
{
    public Type type => GetType();

    public IInventoryItemInfo info { get; }
    public IInventoryItemState state { get; }

    public Item(IInventoryItemInfo info)
    {
        this.info = info;
        state = new InventoryItemState();
    }

    public IInventoryItem Clone()
    {
        var clonnedApple = new Item(info);
        clonnedApple.state.amount = state.amount;
        return clonnedApple;
    }
}
