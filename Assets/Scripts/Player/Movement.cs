using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    [Header("Essential references")]
    [SerializeField] private Animator animator;
    [SerializeField] private SpriteRenderer sprite;
    [SerializeField] private Rigidbody2D rb;

    [Header("Ground check")]
    [SerializeField] private Transform groundCheckTransform;
    [SerializeField] private LayerMask groundLayer;

    [Header("Movement")]
    [SerializeField] private PlayerInput playerInput;
    [SerializeField] private float speed = 8f;
    [SerializeField] private float jumpingPower = 16f;

    private float horizontal;
    private bool isFacingRight = true;

    private InputAction moveAction;
    private InputAction jumpAction;

    private enum MovementState { idle, running, jumping, falling }

    private void Awake()
    {
        moveAction = playerInput.actions["Move"];
        jumpAction = playerInput.actions["Jump"];

        jumpAction.performed += HandleJump;
    }
    private void Update()
    {
        HandleRotate();
        HandleRun();
        UpdateAnimationState();
    }
    private void UpdateAnimationState()
    {
        MovementState state;

        if (horizontal != 0)
        {
            state = MovementState.running;
        }
        else
        {
            state = MovementState.idle;
        }

        if(rb.velocity.y > .1f)
        {
            state = MovementState.jumping;
        }
        else if(rb.velocity.y < -.1f)
        {
            state = MovementState.falling;
        }

        animator.SetInteger("state", (int)state);
    }
    private void HandleRun()
    {
        Vector2 input = moveAction.ReadValue<Vector2>();
        horizontal = input.x;
        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
    }
    private void HandleJump(InputAction.CallbackContext context)
    {
        if(IsGrounded())
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
        }
    }
    private void HandleRotate()
    {
        if (!isFacingRight && horizontal > 0f)
        {
            FlipModel();
        }
        else if (isFacingRight && horizontal < 0f)
        {
            FlipModel();
        }
    }
    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheckTransform.position, 0.2f, groundLayer);
    }

    private void FlipModel()
    {
        isFacingRight = !isFacingRight;
        Vector3 localScale = transform.localScale;
        localScale.x *= -1f;
        transform.localScale = localScale;
    }
}
