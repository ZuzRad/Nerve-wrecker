using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddHP : MonoBehaviour
{
    public Action onTrigger;
    public virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out HealthManager playerHealthManager))
        {
            playerHealthManager.currentHealth++;
            onTrigger?.Invoke();
            Destroy(gameObject);
        }
    }
}
