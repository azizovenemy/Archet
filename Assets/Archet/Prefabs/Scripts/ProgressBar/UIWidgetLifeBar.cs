using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIWidgetLifeBar : MonoBehaviour
{
    [SerializeField] private ProgressBar progressBar;

    private void OnEnable()
    {
        var example = FindObjectOfType<TimerExample>();
        progressBar.SetValue(example.healthNormalized);

        example.OnPlayerHealthValueChangedEvent += OnPlayerHealthValueChanged;
    }

    private void OnPlayerHealthValueChanged(float newValueNormalized)
    {
        progressBar.SetValue(newValueNormalized);
    }

    private void OnDisable()
    {
        var example = FindObjectOfType<TimerExample>();

        if(example)
            example.OnPlayerHealthValueChangedEvent -= OnPlayerHealthValueChanged;
    }
}
