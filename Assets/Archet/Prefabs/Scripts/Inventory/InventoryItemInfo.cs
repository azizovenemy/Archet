using UnityEngine;

[CreateAssetMenu(fileName = "InventoryItemIngo", menuName = "Gameplay/InventoryItems/Create new ItemInfo")]
public class InventoryItemInfo : ScriptableObject, IInventoryItemInfo
{
    [SerializeField] private string _id;
    [SerializeField] private string _title;
    [SerializeField] private string _description;
    [SerializeField] private int _maxItemsInSlot;
    [SerializeField] private Sprite _icon;

    public string id => _id;

    public string title => _title;

    public string description => _description;

    public int maxItemsInInventorySlot => _maxItemsInSlot;

    public Sprite icon => _icon;
}
