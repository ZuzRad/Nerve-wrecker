using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : Obstacle
{
    public bool isRight;
    private Rigidbody2D rb;
    private WaitForSeconds cullDelay = null;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        cullDelay = new WaitForSeconds(3);
        StartCoroutine(DelayedCull());
        if (isRight)
        {
            rb.velocity = transform.right * 15f;
        }
        else
        {
            rb.velocity = -transform.right * 15f;
        }
        
    }

    private IEnumerator DelayedCull()
    {
        yield return cullDelay;
        gameObject.SetActive(false);
        Destroy(gameObject);
    }

    public override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);
        if (isTrigger)
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
    }
}
