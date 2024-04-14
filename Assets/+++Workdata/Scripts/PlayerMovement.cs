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
    public List<Animator> animators;

    private Player_InputActions _inputActions;
    private InputAction _moveAction;
    private InputAction _runAction;
    private Rigidbody2D rb;
    private bool _isMoving;
    private bool _isRunning;

    private float CurrentMoveSpeed => _isRunning ? runSpeed : walkSpeed;

    private void Awake()
    {
        _inputActions = new Player_InputActions();
        _moveAction = _inputActions.Player.Move;
        _runAction = _inputActions.Player.Run;
        rb = GetComponent<Rigidbody2D>();
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

    private void Update()
    {
        Animate();
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
        
        moveInput = context.ReadValue<Vector2>().normalized;
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
    }

    private void Animate()
    {
        foreach (Animator animator in animators)
        {
            animator.SetBool(AnimatorStrings.isMoving, _isMoving);
            animator.SetBool(AnimatorStrings.isRunning, _isRunning);
            if (moveInput != Vector2.zero)
            {
                animator.SetFloat(AnimatorStrings.directionX, moveInput.x);
                animator.SetFloat(AnimatorStrings.directionY, moveInput.y);
            }
        }
    }
}
