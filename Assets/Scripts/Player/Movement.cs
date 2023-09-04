using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class Movement : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private PlayerInput playerInput;
    [SerializeField] public float speed = 8f;
    [SerializeField] private float jumpingPower = 16f;

    [Header("Ground check")]
    [SerializeField] private Transform groundCheckTransform;
    [SerializeField] private LayerMask groundLayer;

    private Rigidbody2D rb;

    [HideInInspector] public float horizontal;
    private bool isFacingRight = true;

    private InputAction moveAction;
    private InputAction jumpAction;
    public Action onJump;

    public float additionalForce = 0;


    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void Awake()
    {
        moveAction = playerInput.actions["Move"];
        jumpAction = playerInput.actions["Jump"];

        jumpAction.performed += HandleJump;
    }

    private void OnDisable()
    {
        jumpAction.performed -= HandleJump;
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
        
        if(additionalForce != 0)
        {
            rb.AddForce(new Vector2(horizontal * additionalForce, 0f));
        }
        else
        {
            rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
        }
    }
    private void HandleJump(InputAction.CallbackContext context)
    {
        if(IsGrounded())
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
            onJump?.Invoke();
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
        return Physics2D.OverlapCircle(groundCheckTransform.position, 0.4f, groundLayer);
    }

    private void FlipModel()
    {
        isFacingRight = !isFacingRight;
        Vector3 localScale = transform.localScale;
        localScale.x *= -1f;
        transform.localScale = localScale;
    }

    //public void AddForce(float force)
    //{
    //    Debug.Log(force);
    //    rb.velocity *= force;
    //   // rb.AddForce(new Vector2(horizontal * force, 0f));
    //}
}
