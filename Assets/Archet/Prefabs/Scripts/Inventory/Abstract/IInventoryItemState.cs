using System;
using UnityEngine;

public interface IInventoryItemState
{
    int amount { get; set; }
    bool isEquipped { get; set; }
}
