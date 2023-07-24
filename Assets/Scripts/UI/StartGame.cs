using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGame : MonoBehaviour
{
    public void StartGameLevel()
    {
        // Load the game scene
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainGame");
    }
}
