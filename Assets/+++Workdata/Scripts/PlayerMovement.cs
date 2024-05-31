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
    public float accelerationTime = 0.075f;

    public bool isOnStairs;
    public float stairSlope;

    public Player_InputActions _inputActions;
    private InputAction _moveAction;
    private InputAction _runAction;
    private InputAction _interactAction;
    private InputAction _attackAction;
    private Rigidbody2D rb;
    private bool _isMoving;
    private bool _isRunning;
    private Interactable _selectedInteractable;
    private Vector2 _currentVelocity;

    private float CurrentMoveSpeed => _isRunning ? runSpeed : walkSpeed;


    #region Component Lifecycle

    private void Awake()
    {
        _inputActions = new Player_InputActions();
        _moveAction = _inputActions.Player.Move;
        _runAction = _inputActions.Player.Run;
        _interactAction = _inputActions.Player.Interact;
        _attackAction = _inputActions.Player.Attack;
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        EnableInput();
        _moveAction.performed += Move;
        _moveAction.canceled += Move;
        _runAction.performed += Run;
        _runAction.canceled += Run;
        _interactAction.performed += Interact;
        _attackAction.performed += Attack;
    }

    private void OnDisable()
    {
        DisableInput();
        _moveAction.performed -= Move;
        _moveAction.canceled -= Move;
        _runAction.performed -= Run;
        _runAction.canceled -= Run;
        _interactAction.performed -= Interact;
        _attackAction.performed -= Attack;
    }

    private void FixedUpdate()
    {
        Vector2 targetVelocity = moveInput * CurrentMoveSpeed;
        // add the stair slope to the players velocity when they are on a stair
        if (isOnStairs && moveInput.magnitude > 0)
        {
            targetVelocity = new Vector2(
                moveInput.x * CurrentMoveSpeed,
                (moveInput.y - moveInput.x * stairSlope) * CurrentMoveSpeed
            );
        }

        // set player velocity with acceleration
        rb.velocity = Vector2.SmoothDamp(rb.velocity, targetVelocity, ref _currentVelocity, accelerationTime);

        if (_inputActions.Player.enabled)
        {
            Animate();
        }
    }

    #endregion

    #region Animation

    private void Animate()
    {
        foreach (Animator animator in animators)
        {
            if (animator.gameObject.activeInHierarchy)
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

    #endregion

    #region Input Actions

    public void EnableInput()
    {
        _inputActions.Enable();
    }

    public void DisableInput()
    {
        _inputActions.Disable();
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

    private void Interact(InputAction.CallbackContext context)
    {
        if (_selectedInteractable != null)
        {
            _selectedInteractable.Interact();
        }
    }

    private void Attack(InputAction.CallbackContext context)
    {
        foreach (Animator animator in animators)
        {
            if (animator.gameObject.activeInHierarchy)
            {
                animator.SetTrigger(AnimatorStrings.attack);
            }
        }
    }

    #endregion

    #region Interactions

    private void OnTriggerEnter2D(Collider2D col)
    {
        TrySelectInteractable(col);
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        TryDeselectInteractable(col);
    }

    private void TrySelectInteractable(Collider2D col)
    {
        Interactable interactable = col.GetComponent<Interactable>();
        if (interactable == null) return;

        if (_selectedInteractable != null)
        {
            _selectedInteractable.Deselect();
        }

        _selectedInteractable = interactable;
        _selectedInteractable.Select();
    }

    private void TryDeselectInteractable(Collider2D col)
    {
        Interactable interactable = col.GetComponent<Interactable>();
        if (interactable == null) return;

        if (interactable == _selectedInteractable)
        {
            _selectedInteractable.Deselect();
            _selectedInteractable = null;
        }
    }

    #endregion
}