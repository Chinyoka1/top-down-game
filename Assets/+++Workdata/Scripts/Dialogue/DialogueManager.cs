using System;
using System.Collections;
using System.Collections.Generic;
using Ink.Runtime;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class DialogueManager : MonoBehaviour
{
    private static DialogueManager instance;
    private Story currentStory;
    private Player_InputActions _inputActions;
    private InputAction _continueAction;

    public bool isPlaying;
    public GameObject dialoguePanel;
    public TextMeshProUGUI dialogueText;

    public static DialogueManager GetInstance()
    {
        return instance;
    }
    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Found more than one instance of Dialogue Manager");
        }
        instance = this;
        _inputActions = new Player_InputActions();
        _continueAction = _inputActions.Player.Continue;
    }
    
    #region Input System

    private void OnEnable()
    {
        _inputActions.Enable();
        _continueAction.performed += Continue;
        _continueAction.canceled += Continue;
    }

    private void OnDisable()
    {
        _inputActions.Disable();
        _continueAction.performed -= Continue;
        _continueAction.canceled -= Continue;
    }

    #endregion

    private void Start()
    {
        isPlaying = false;
        dialoguePanel.SetActive(false);
    }

    private void Update()
    {
        if (!isPlaying)
        {
            return;
        }
    }

    public void EnterDialogue(TextAsset inkJSON)
    {
        currentStory = new Story(inkJSON.text);
        isPlaying = true;
        dialoguePanel.SetActive(true);
        ContinueStory();
    }

    private void ExitDialogue()
    {
        isPlaying = false;
        dialoguePanel.SetActive(false);
        dialogueText.text = "";
    }

    private void Continue(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            ContinueStory();
        }
    }

    private void ContinueStory()
    {
        if (currentStory.canContinue)
        {
            dialogueText.text = currentStory.Continue();
        }
        else
        {
            ExitDialogue();
        }
    }
}
