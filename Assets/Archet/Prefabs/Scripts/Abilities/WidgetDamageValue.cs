using TMPro;
using UnityEngine;

public class WidgetDamageValue : MonoBehaviour
{
    [SerializeField] private TMP_Text _textValue;

    public void SetValue(string newValue)
    {
        _textValue.text = newValue;
    }

    public void SetColor(Color newColor)
    {
        _textValue.color = newColor;
    }

    private void Handle_AnimationOver()
    {
        Destroy(gameObject);
    }
}