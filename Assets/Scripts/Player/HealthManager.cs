using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
    public int currentHealth;
    public int maxHealth = 3;

    public Action onPlayerDeath;
    public Action onPlayerLost;
    public Action onIncreaseHealth;

    private void Start()
    {
        currentHealth = maxHealth;
    }

    public void IncreaseHealth()
    {
        currentHealth++;
        onIncreaseHealth?.Invoke();
    }
    public void DecreaseHealth()
    {
        if (--currentHealth == 0)
        {
            onPlayerLost?.Invoke();
        }
        onPlayerDeath?.Invoke();
    }
}
