using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootLaser : MonoBehaviour
{
    [SerializeField] private GameObject laserPrefab;
    [HideInInspector] public LaserBeam laserBeam;
    private GameObject laser;

    public Action onTrigger;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out HealthManager player))
        {
            laser = Instantiate(laserPrefab);
            laserBeam = laser.GetComponent<LaserBeam>();
            laserBeam.target = player.gameObject;
            laserBeam.startPosition = transform;
            laserBeam.onTrigger += OnTriggerLaser;
        }
    }

    private void OnTriggerLaser()
    {
        onTrigger?.Invoke();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Movement player))
        {
            Destroy(laser);
            laserBeam.onTrigger -= OnTriggerLaser;
        }
    }
}
