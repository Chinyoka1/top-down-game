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
    
    public bool isOnStairs;
    public float stairSlope;

    private Player_InputActions _inputActions;
    private InputAction _moveAction;
    private InputAction _runAction;
    private InputAction _interactAction;
    private Rigidbody2D rb;
    private bool _isMoving;
    private bool _isRunning;
    private Interactable _selectedInteractable;

    private float CurrentMoveSpeed => _isRunning ? runSpeed : walkSpeed;


    #region Unity Lifecycle

    private void Awake()
    {
        _inputActions = new Player_InputActions();
        _moveAction = _inputActions.Player.Move;
        _runAction = _inputActions.Player.Run;
        _interactAction = _inputActions.Player.Interact;
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
    }

    private void OnDisable()
    {
        DisableInput();
        _moveAction.performed -= Move;
        _moveAction.canceled -= Move;
        _runAction.performed -= Run;
        _runAction.canceled -= Run;
        _interactAction.performed -= Interact;
    }

    private void Update()
    {
        if (isOnStairs)
        {
            rb.velocity = new Vector2(
                moveInput.x * CurrentMoveSpeed, 
                (moveInput.y - moveInput.x * stairSlope) * CurrentMoveSpeed
            );
        }
        else
        {
            rb.velocity = new Vector2(moveInput.x * CurrentMoveSpeed, moveInput.y * CurrentMoveSpeed);
        }
        if (_inputActions.Player.enabled)
        {
            Animate();
        }
    }

    private void FixedUpdate()
    {
        
        
    }

    #endregion

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

    public void EnableInput()
    {
        _inputActions.Enable();
    }
    
    public void DisableInput()
    {
        _inputActions.Disable();
    }

    #region Input Actions

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

    #endregion

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
}
