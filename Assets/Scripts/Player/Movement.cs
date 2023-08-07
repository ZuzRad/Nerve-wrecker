using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class Movement : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private PlayerInput playerInput;
    [SerializeField] private float speed = 8f;
    [SerializeField] private float jumpingPower = 16f;

    [Header("Ground check")]
    [SerializeField] private Transform groundCheckTransform;
    [SerializeField] private LayerMask groundLayer;

    private Rigidbody2D rb;

    [HideInInspector] public float horizontal;
    private bool isFacingRight = true;

    private InputAction moveAction;
    private InputAction jumpAction;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void OnDisable()
    {
        Destroy(this);
    }
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
        if (groundCheckTransform != null)
        {
            return Physics2D.OverlapCircle(groundCheckTransform.position, 0.2f, groundLayer);
        }
        return false;
    }

    private void FlipModel()
    {
        isFacingRight = !isFacingRight;
        Vector3 localScale = transform.localScale;
        localScale.x *= -1f;
        transform.localScale = localScale;
    }
}
