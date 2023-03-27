using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ProgressBar : MonoBehaviour
{
    [SerializeField] private Image imageFiller;
    [SerializeField] private TMP_Text textValue;

    public void SetValue(float valueNormalized)
    {
        imageFiller.fillAmount = valueNormalized;

        var valueInPercent = Mathf.RoundToInt(valueNormalized * 100f);
        textValue.text = $"{valueInPercent}%";
    }
}
