using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserBeam : MonoBehaviour
{
    LineRenderer laser;
    [HideInInspector] public GameObject target;
    [HideInInspector] public Transform startPosition;

    private void Start()
    {
        laser = gameObject.GetComponent<LineRenderer>();

        laser.startWidth = 0.1f;
        laser.endWidth = 0.1f;
        laser.startColor = new Color32(245, 207, 130, 255);
        laser.endColor = new Color32(245, 207, 130, 255);

        laser.SetPosition(0, startPosition.position);
        laser.SetPosition(1, target.transform.position);
    }


    public void Update()
    {
        laser.SetPosition(1, target.transform.position);
    }
}

