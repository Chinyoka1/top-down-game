using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveManager : MonoBehaviour
{
    public void SaveGame()
    {
        Transform player = FindObjectOfType<PlayerMovement>().transform;
        PlayerPrefs.SetFloat("PlayerPosX", player.position.x);
        PlayerPrefs.SetFloat("PlayerPosY", player.position.y);
        PlayerPrefs.SetString("Scene", SceneManager.GetActiveScene().name);
        
        PlayerPrefs.Save();
    }

    public void LoadGame()
    {
        float posX = PlayerPrefs.GetFloat("PlayerPosX");
        float posY = PlayerPrefs.GetFloat("PlayerPosY");
        string sceneName = PlayerPrefs.GetString("Scene");

        //SceneManager.LoadScene(sceneName);
        Transform player = FindObjectOfType<PlayerMovement>().transform;
        player.position = new Vector3(posX, posY, 0);
    }
}
