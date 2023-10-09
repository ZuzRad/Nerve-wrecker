using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    //protected bool isTrigger = false;
    public virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent(out HealthManager playerHealthManager) && !playerHealthManager.isOnCooldown)
        {
           // isTrigger = true;
            playerHealthManager.DecreaseHealth();
        }
    }
}
