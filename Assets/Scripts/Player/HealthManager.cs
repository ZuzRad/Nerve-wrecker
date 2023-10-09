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

    public bool isOnCooldown = false;

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
        isOnCooldown = true;
        if (--currentHealth == 0)
        {
            onPlayerLost?.Invoke();
        }
        onPlayerDeath?.Invoke();
        StartCoroutine(WaitAndDisableCooldown());
    }

    private IEnumerator WaitAndDisableCooldown()
    {
        yield return new WaitForSeconds(0.5f);
        isOnCooldown = false;
    }
}
