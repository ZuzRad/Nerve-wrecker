using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikedHead : Obstacle
{
    [SerializeField] GameObject projectile;
    [SerializeField] Transform spawnPoint;
    [SerializeField] float timer = 0.5f;
    [SerializeField] int projectileAmount = 2;
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
                projectile.GetComponent<Projectile>().isRight = i % 2 == 0;
                projectileGO.transform.position = spawnPoint.position;
            }
            timer = tempTimer;
        }
    }
}
