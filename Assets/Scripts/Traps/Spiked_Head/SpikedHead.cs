using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikedHead : Obstacle
{
    [SerializeField] GameObject projectile;
    [SerializeField] Transform spawnPoint;
    [SerializeField] float timer = 0.5f;
    [SerializeField] int projectileAmount = 2;
    [SerializeField] bool onlyOneWay = false;
    [SerializeField] bool isRight = false;
    private Animator animator;

    private float tempTimer;
    private void Start()
    {
        animator = GetComponent<Animator>();
        tempTimer = timer;
    }
    private void Update()
    {
        timer -= 1 * Time.deltaTime;
        if(timer <= 0)
        {
            animator.SetTrigger("shoot");
            for(int i = 0; i < projectileAmount; i++)
            {
                GameObject projectileGO = Instantiate(projectile);
                Projectile projectileScript = projectileGO.GetComponent<Projectile>();
                projectileScript.head = GetComponent<BoxCollider2D>();
                if (onlyOneWay)
                {
                    if (isRight)
                    {
                        projectileScript.isRight = true;
                    }
                    else
                    {
                        projectileScript.isRight = false;
                    }
                }
                else
                {
                    projectileScript.isRight = i % 2 == 0;
                }

                projectileGO.transform.position = spawnPoint.position;
            }
            timer = tempTimer;
        }
    }
}
