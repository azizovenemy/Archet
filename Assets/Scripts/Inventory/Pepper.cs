using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pepper : IInventoryItem
{
    public Type type => GetType();

    public IInventoryItemInfo info { get; }

    public IInventoryItemState state { get; }

    public Pepper(IInventoryItemInfo info)
    {
        this.info = info;
        state = new InventoryItemState();
    }

    public IInventoryItem Clone()
    {
        var clonnedPepper = new Pepper(info);
        clonnedPepper.state.amount = state.amount;
        return clonnedPepper;
    }
}
