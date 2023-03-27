using System;
using UnityEngine;

public class PlayerDamage : MonoBehaviour
{
    public event Action<float> OnPlayerHealthValueChangedEvent;

    public int health { get; private set; }
    public float healthNormalized => (float) healthDefault / health;

    private int healthDefault => health;

    public void HitPlayer()
    {
        health = 100;

        if(health <= 0)
        {
            Destroy(gameObject);
        }

        OnPlayerHealthValueChangedEvent?.Invoke(healthNormalized);
    }
}
