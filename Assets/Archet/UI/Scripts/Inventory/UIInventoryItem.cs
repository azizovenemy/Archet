using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIInventoryItem : UIItem
{
    [SerializeField] private Image _imageIcon;
    [SerializeField] private TMP_Text _textAmount;

    public IInventoryItem item { get; private set; }

    public void Refresh(IInventorySlot slot)
    {
        if (slot.isEmpty)
        {
            CleanUp();
            return;
        }

        item = slot.item;
        _imageIcon.sprite = item.info.icon;
        _imageIcon.gameObject.SetActive(true);

        var textAmountEnabled = slot.amount > 1;

        _textAmount.gameObject.SetActive(textAmountEnabled);
        
        if (textAmountEnabled)
            _textAmount.text = $"x{slot.amount}";

    }

    private void CleanUp()
    {
        _imageIcon.gameObject.SetActive(false);
        _textAmount.gameObject.SetActive(false);
    }
}
