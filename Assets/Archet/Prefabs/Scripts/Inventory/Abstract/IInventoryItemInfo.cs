using UnityEngine;

public interface IInventoryItemInfo
{
    string id { get; }
    string title { get; }
    string description { get; }
    int maxItemsInInventorySlot { get; }
    Sprite icon { get; }
}
