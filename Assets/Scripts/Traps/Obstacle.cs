using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public bool isCoolDownOn = false;
    public virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent(out HealthManager playerHealthManager) && !playerHealthManager.isOnCooldown)
        {
            //if (isCoolDownOn)
            //{
            //    StartCoroutine(WaitAndDecreaseHealth(playerHealthManager));
            //}
            //else
            //{
                playerHealthManager.DecreaseHealth();
           // }
        }
    }

    //private IEnumerator WaitAndDecreaseHealth(HealthManager playerHealthManager)
    //{
    //    yield return new WaitForSeconds(0.5f);
    //    playerHealthManager.DecreaseHealth();
    //}
}
