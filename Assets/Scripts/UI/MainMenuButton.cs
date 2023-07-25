using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuButton : MonoBehaviour
{

    public void StartMainMenuLevel()
    {
        // Load the game scene
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
        
        //dont destroy the music
        DontDestroyOnLoad(GameObject.Find("Music"));
    }
}
