using System;
using UnityEngine;

public class Test : MonoBehaviour
{
    public event Action<float> OnPlayerHealthValueChangedEvent;

    public int health { get; private set; }
    public float healthNormalized => (float) health / healthDefault;
    
    [SerializeField] private int healthDefault;

    private void Awake()
    {
        health = healthDefault;
        OnPlayerHealthValueChangedEvent?.Invoke(healthNormalized);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            this.HitPlayer();
        }
    }

    private void HitPlayer()
    {
        health -= 5;

        if(health <= 0)
        {
            Destroy(gameObject);
        }

        OnPlayerHealthValueChangedEvent?.Invoke(healthNormalized);
    }
}
