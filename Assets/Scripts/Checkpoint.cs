using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    [HideInInspector]public Animator animator;
    public Action<Checkpoint> onCheckpointTrigger;
    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Movement player))
        {
            onCheckpointTrigger?.Invoke(this);
        }
    }
}
