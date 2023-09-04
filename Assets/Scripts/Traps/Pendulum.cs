using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pendulum : MonoBehaviour
{
    [SerializeField] private float speed = 1.5f;
    [SerializeField] private float limit = 75f;
    [SerializeField] private Transform lineTransform;

    private void Update()
    {
        float angle = limit * Mathf.Sin(Time.time * speed);
        lineTransform.localRotation = Quaternion.Euler(0, 0, angle);
    }
}
