using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator), typeof(Rigidbody2D), typeof(HealthManager))]
public class AnimationsController : MonoBehaviour
{
    private Animator animator;
    private Movement playerMovement;
    private Rigidbody2D rb;
    private HealthManager healthManager;

    private enum MovementState { idle, running, jumping, falling }
    private void OnDisable()
    {
        healthManager.onPlayerDeath -= SetDeathAnimation;
    }
    private void Start()
    {
        healthManager = GetComponent<HealthManager>();
        playerMovement = GetComponent<Movement>();
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();

        healthManager.onPlayerDeath += SetDeathAnimation;
    }

    private void SetDeathAnimation()
    {
        animator.SetTrigger("death");
    }

    private void Update()
    {
        UpdateAnimationState();
    }

    private void UpdateAnimationState()
    {
        MovementState state;

        if (playerMovement.horizontal != 0)
        {
            state = MovementState.running;
        }
        else
        {
            state = MovementState.idle;
        }

        if (rb.velocity.y > .1f)
        {
            state = MovementState.jumping;
        }
        else if (rb.velocity.y < -.1f)
        {
            state = MovementState.falling;
        }

        animator.SetInteger("state", (int)state);
    }
}
