using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public Vector2 moveInput;
    public float walkSpeed = 4f;
    public float runSpeed = 7f;

    private Player_InputActions _inputActions;
    private InputAction _moveAction;
    private InputAction _runAction;
    private Rigidbody2D rb;
    private Animator animator;
    private Vector2 lastMoveDirection;
    private bool _isMoving;
    private bool _isRunning;

    private float CurrentMoveSpeed => _isRunning ? runSpeed : walkSpeed;

    private void Awake()
    {
        _inputActions = new Player_InputActions();
        _moveAction = _inputActions.Player.Move;
        _runAction = _inputActions.Player.Run;
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        _inputActions.Enable();
        _moveAction.performed += Move;
        _moveAction.canceled += Move;
        _runAction.performed += Run;
        _runAction.canceled += Run;
    }

    private void OnDisable()
    {
        _inputActions.Disable();
        _moveAction.performed -= Move;
        _moveAction.canceled -= Move;
        _runAction.performed -= Run;
        _runAction.canceled -= Run;
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(moveInput.x * CurrentMoveSpeed, moveInput.y * CurrentMoveSpeed);
    }

    private void Move(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            _isMoving = true;
        }
        if (context.canceled)
        {
            _isMoving = false;
        }
        animator.SetBool("isMoving", _isMoving);

        // save last move input
        if (!_isMoving && moveInput.x != 0 || moveInput.y != 0)
        {
            lastMoveDirection = moveInput;
            animator.SetFloat("lastMoveDirectionX", lastMoveDirection.x);
            animator.SetFloat("lastMoveDirectionY", lastMoveDirection.y);
        }
        // set new move input
        moveInput = context.ReadValue<Vector2>().normalized;
        animator.SetFloat("directionX", moveInput.x);
        animator.SetFloat("directionY", moveInput.y);
    }

    private void Run(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            _isRunning = true;
        }
        if (context.canceled)
        {
            _isRunning = false;
        }
        animator.SetBool("isRunning", _isRunning);
    }
}
