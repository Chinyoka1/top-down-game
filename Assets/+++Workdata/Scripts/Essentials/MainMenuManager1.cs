using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager1 : MonoBehaviour
{
    private GameController gameController;

    private void OnEnable()
    {
        gameController = FindObjectOfType<GameController>();

        switch (gameController.gameMode)
        {
            case GameController.GameMode.PreMenu:
            {
                gameController.gameMode = GameController.GameMode.MainMenu;
                break;
            }
            case GameController.GameMode.LoadScene:
            {
                gameController.gameMode = GameController.GameMode.MainMenu;
                break;
            }
            default:
            {
                Debug.LogWarning("Wrong game mode detected: " + gameController.gameMode);
                gameController.gameMode = GameController.GameMode.MainMenu;
                break;
            }
        }
    }

    public void Button_NewGame()
    {
        gameController.gameMode = GameController.GameMode.NewGame;
        SceneManager.LoadScene("GameplayScene");
    }

    public void Button_Continue()
    {
        gameController.gameMode = GameController.GameMode.LoadSaveGame;
        //SceneManager.LoadScene(PlayerPrefs.GetString("Scene"));
        FindObjectOfType<SaveManager>().LoadGame();
    }

    public void Button_Quit()
    {
        Application.Quit();
    }
}
