using UnityEngine;

public class UIWidgetLifeBar : MonoBehaviour
{
    [SerializeField] private ProgressBar progressBar;
    [SerializeField] private NPC player;

    private void OnEnable()
    {
        progressBar.SetValue(player.health);

        player.OnPlayerHealthValueChangedEvent += OnPlayerHealthValueChanged;
    }

    private void OnPlayerHealthValueChanged(float newValueNormalized)
    {
        progressBar.SetValue(newValueNormalized);
    }

    private void OnDisable()
    {
        if(player)
            player.OnPlayerHealthValueChangedEvent -= OnPlayerHealthValueChanged;
    }
}
