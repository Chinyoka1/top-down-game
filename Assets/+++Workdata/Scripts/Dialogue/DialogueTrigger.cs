using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DialogueTrigger : MonoBehaviour
{
    public GameObject icon;
    private bool playerInRange;
    public TextAsset inkJSON;
    private Player_InputActions _inputActions;
    private InputAction _interactAction;

    private void Awake()
    {
        icon.SetActive(false);
        _inputActions = new Player_InputActions();
        _interactAction = _inputActions.Player.Interact;
    }

    #region Input System

    private void OnEnable()
    {
        _inputActions.Enable();
        _interactAction.performed += Interact;
        _interactAction.canceled += Interact;
    }

    private void OnDisable()
    {
        _inputActions.Disable();
        _interactAction.performed -= Interact;
        _interactAction.canceled -= Interact;
    }

    #endregion

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            playerInRange = true;
            icon.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            playerInRange = false;
            icon.SetActive(false);
        }
    }

    private void Interact(InputAction.CallbackContext context)
    {
        if (playerInRange)
        {
            if (context.performed)
            {
                DialogueManager.GetInstance().EnterDialogue(inkJSON);
            }
        }
    }
}
