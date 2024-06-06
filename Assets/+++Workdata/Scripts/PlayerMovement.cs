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

    private Rigidbody2D rb;
    private bool _isMoving;
    private bool _isRunning;
    private Vector2 _currentVelocity;
    [SerializeField] private List<Interactable> selectedInteractables;
    [SerializeField] private InputReader inputReader;

    private float CurrentMoveSpeed => _isRunning ? runSpeed : walkSpeed;


    #region Component Lifecycle

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        EnableInput();
        inputReader.moveAction.performed += Move;
        inputReader.moveAction.canceled += Move;
        inputReader.runAction.performed += Run;
        inputReader.runAction.canceled += Run;
        inputReader.interactAction.performed += Interact;
        inputReader.attackAction.performed += Attack;
    }

    private void OnDisable()
    {
        DisableInput();
        inputReader.moveAction.performed -= Move;
        inputReader.moveAction.canceled -= Move;
        inputReader.runAction.performed -= Run;
        inputReader.runAction.canceled -= Run;
        inputReader.interactAction.performed -= Interact;
        inputReader.attackAction.performed -= Attack;
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

        if (inputReader.GetPlayerEnabled())
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
        inputReader.Enable();
    }

    public void DisableInput()
    {
        inputReader.Disable();
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
        if (selectedInteractables.Count > 0)
        {
            selectedInteractables[0].Interact();
            StartCoroutine(CheckLastInteraction());
        }
    }

    IEnumerator CheckLastInteraction()
    {
        yield return null;
        if (selectedInteractables.Count > 0 && selectedInteractables[0] == null)
        {
            selectedInteractables.RemoveAt(0);
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

        selectedInteractables.Add(interactable);
        interactable.Select();
    }

    private void TryDeselectInteractable(Collider2D col)
    {
        Interactable interactable = col.GetComponent<Interactable>();
        if (interactable == null) return;

        //if (selectedInteractables.Contains(interactable))
        //{
        //    interactable.Deselect();
        //    selectedInteractables.RemoveAt(selectedInteractables.IndexOf(interactable));
        //}

        for (int i = 0; i < selectedInteractables.Count; i++)
        {
            if (selectedInteractables[i] == interactable)
            {
                selectedInteractables[i].Deselect();
                selectedInteractables.RemoveAt(i);
            }
        }
    }

    #endregion
}