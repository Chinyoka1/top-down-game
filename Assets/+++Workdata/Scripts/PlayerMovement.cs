using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public Vector2 moveInput;
    public float walkSpeed = 5f;

    private Player_InputActions _inputActions;
    private InputAction _moveAction;
    private Rigidbody2D rb;
    private Animator animator;

    private void Awake()
    {
        _inputActions = new Player_InputActions();
        _moveAction = _inputActions.Player.Move;
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        _inputActions.Enable();
        _moveAction.performed += OnMove;
        _moveAction.canceled += OnMove;
    }

    private void OnDisable()
    {
        _inputActions.Disable();
        _moveAction.performed -= OnMove;
        _moveAction.canceled -= OnMove;
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(moveInput.x * walkSpeed, moveInput.y * walkSpeed);
    }

    private void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>().normalized;
    }
}
