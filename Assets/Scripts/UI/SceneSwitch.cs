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
        
        //dont destroy the sfx
        DontDestroyOnLoad(GameObject.Find("SFX"));

    }
    
    public void StartGameLevel()
    {
        // Load the game scene
        // print("GAME");
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainGame");
        
        //dont destroy the music
        DontDestroyOnLoad(GameObject.Find("Music"));
        
        //dont destroy the sfx
        DontDestroyOnLoad(GameObject.Find("SFX"));
        
    }
    
    public void StartShopMenuLevel()
    {
        // print("SHOP MENU");
        // Load the game scene
        UnityEngine.SceneManagement.SceneManager.LoadScene("ShopScene");
        
        //dont destroy the music
        DontDestroyOnLoad(GameObject.Find("Music"));
        
        //dont destroy the sfx
        DontDestroyOnLoad(GameObject.Find("SFX"));
    }

    public void StartSettingsMenuLevel()
    {
        
        // Load the game scene
        // print("SETTINGS MENU");
        UnityEngine.SceneManagement.SceneManager.LoadScene("SettingsScene");
        
        //dont destroy the music
        DontDestroyOnLoad(GameObject.Find("Music"));
        
        //dont destroy the sfx
        DontDestroyOnLoad(GameObject.Find("SFX"));
    }
    
    public void StartCreditsMenuLevel()
    {
        // Load the game scene
        // print("CREDITS MENU");
        UnityEngine.SceneManagement.SceneManager.LoadScene("CreditsScene");
        
        //dont destroy the music
        DontDestroyOnLoad(GameObject.Find("Music"));
        
        //dont destroy the sfx
        DontDestroyOnLoad(GameObject.Find("SFX"));
    }
    
}
