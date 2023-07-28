using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneSwitch : MonoBehaviour
{
    public void StartMainMenuLevel()
    {
        // Load the game scene
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");

        //dont destroy the music
        DontDestroyOnLoad(GameObject.Find("Music"));

    }
    
    public void StartGameLevel()
    {
        // Load the game scene
        print("GAME");
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainGame");
        
        //dont destroy the music
        DontDestroyOnLoad(GameObject.Find("Music"));
    }
    
    public void StartShopMenuLevel()
    {
        print("SHOP MENU");
        // Load the game scene
        UnityEngine.SceneManagement.SceneManager.LoadScene("ShopScene");
        
        //dont destroy the music
        DontDestroyOnLoad(GameObject.Find("Music"));
    }

    public void StartSettingsMenuLevel()
    {
        
        // Load the game scene
        print("SETTINGS MENU");
        UnityEngine.SceneManagement.SceneManager.LoadScene("SettingsScene");
        
        //dont destroy the music
        DontDestroyOnLoad(GameObject.Find("Music"));
    }
}
