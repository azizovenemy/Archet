using System;
using UnityEngine;

public class NPC : MonoBehaviour, IEnemy
{
    public event Action<float> OnPlayerHealthValueChangedEvent;

    public int health { get; private set; }

    public float healthNormalized => (float)health / healthDefault;

    private int healthDefault;

    private void Start()
    {
        health = 100;
        healthDefault = health;
        OnPlayerHealthValueChangedEvent?.Invoke(healthNormalized);
    }

    public void HitEnemy(float damage)
    {
        health -= (int)damage;

        if (health <= 0)
        {
            Destroy(gameObject);
        }

        OnPlayerHealthValueChangedEvent?.Invoke(healthNormalized);
    }
}
