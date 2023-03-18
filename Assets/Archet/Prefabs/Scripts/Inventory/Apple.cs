using System;

public class Apple : IInventoryItem
{
    public Type type => GetType();

    public IInventoryItemInfo info { get; }

    public IInventoryItemState state { get; }

    public Apple(IInventoryItemInfo info)
    {
        this.info = info;
        state = new InventoryItemState();
    }

    public IInventoryItem Clone()
    {
        var clonnedApple = new Apple(info);
        clonnedApple.state.amount = state.amount;
        return clonnedApple;
    }
}
