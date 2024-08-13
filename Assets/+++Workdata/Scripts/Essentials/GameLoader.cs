using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLoader : MonoBehaviour
{
    public Transform[] spawnPoints;
    private Transform player;
    private SpawnPointSaver spawnPointSaver;
    
    private void OnEnable()
    {
        GameController gameController = FindObjectOfType<GameController>();
        spawnPointSaver = FindObjectOfType<SpawnPointSaver>();
        player = FindObjectOfType<PlayerMovement>().transform;
        
        switch (gameController.gameMode)
        {
            case GameController.GameMode.NewGame:
            {
                player.position = spawnPoints[0].position;
                break;
            }
            case GameController.GameMode.LoadSaveGame:
            {
                // performanter wäre mit ActionEvent
                //FindObjectOfType<SaveManager>().LoadGame();
                break;
            }
            case GameController.GameMode.LoadScene:
            {
                player.position = spawnPoints[spawnPointSaver.spawnPointId].position;
                break;
            }
            case GameController.GameMode.DebugMode:
            {
                Debug.LogWarning("Debug Mode turned on");
                break;
            }
            default:
            {
                Debug.LogWarning("Game Mode not supported");
                break;
            }
        }
    }
}
