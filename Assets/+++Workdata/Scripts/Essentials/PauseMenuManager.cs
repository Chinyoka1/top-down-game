using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PauseMenuManager : MonoBehaviour
{
    [SerializeField] private InputReader inputReader;
    [SerializeField] private GameObject pauseMenuContainer;
    private GameController gameController;

    private void Awake()
    {
        gameController = FindObjectOfType<GameController>();
    }

    private void OnEnable()
    {
        inputReader.pauseAction.performed += TogglePauseMenu;
    }

    private void OnDisable()
    {
        inputReader.pauseAction.performed -= TogglePauseMenu;
    }

    private void TogglePauseMenu(InputAction.CallbackContext context)
    {
        pauseMenuContainer.SetActive(!pauseMenuContainer.activeSelf);
        if (pauseMenuContainer.activeSelf)
        {
            gameController.EnterPauseMode();
        }
        else
        {
            gameController.EnterPlayMode();
        }
    }
}
