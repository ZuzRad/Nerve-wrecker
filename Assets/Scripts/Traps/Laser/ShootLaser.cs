using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootLaser : MonoBehaviour
{
    [SerializeField] private GameObject laserPrefab;
    private GameObject laser;
    public Action onTrigger;

    private void OnTriggerEnter2D(Collider2D collision) //TODO zmniejszanie ¿ycia
    {
        if (collision.TryGetComponent(out HealthManager player))
        {
            laser = Instantiate(laserPrefab);
            LaserBeam laserBeam = laser.GetComponent<LaserBeam>();
            laserBeam.target = player.gameObject;
            laserBeam.startPosition = transform;
            onTrigger?.Invoke();
            //player.DecreaseHealth();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Movement player))
        {
            Destroy(laser);
        }
    }
}
