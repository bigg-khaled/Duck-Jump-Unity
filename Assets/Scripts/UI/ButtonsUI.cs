using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonsUI : MonoBehaviour
{
    public void StartMainMenuLevel()
    {
        // Load the game scene
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
        
        //dont destroy the music
        DontDestroyOnLoad(GameObject.FindWithTag("Music"));
    }
    
    public void StartGameLevel()
    {
        // Load the game scene
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainGame");
        
        //dont destroy the music
        DontDestroyOnLoad(GameObject.Find("Music"));
    }
    
    public void StartShopMenuLevel()
    {
        // Load the game scene
        UnityEngine.SceneManagement.SceneManager.LoadScene("ShopScene");
        
        //dont destroy the music
        DontDestroyOnLoad(GameObject.Find("Music"));
    }

    public void StartSettingsMenuLevel()
    {
        
        // Load the game scene
        UnityEngine.SceneManagement.SceneManager.LoadScene("SettingsScene");
        
        //dont destroy the music
        DontDestroyOnLoad(GameObject.Find("Music"));
    }
}
