using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenu;
    public GameObject pauseButton;
    public  Image musicOn;
    public Image musicOff;
    private bool musicEnabled;
    public Image sfxOn;
    public Image sfxOff;
    private bool sfxEnabled;

    private void Start()
    {
        musicOn = GameObject.Find("MusicOnIcon").GetComponent<Image>();
        musicOff = GameObject.Find("MusicOffIcon").GetComponent<Image>();
        sfxOn = GameObject.Find("SFXOnIcon").GetComponent<Image>();
        sfxOff = GameObject.Find("SFXOffIcon").GetComponent<Image>();
    }

    public void PauseGame()
    {
        // hide the pause button
        gameObject.SetActive(false);
        
        // show the pause menu
        pauseMenu.SetActive(true);
        
        //stop the game
        Time.timeScale = 0;
        
        Debug.Log("Pause Game");
    }
    
    public void ResumeGame()
    {
        // hide the pause menu
        pauseMenu.SetActive(false);
        
        // show the pause button
        pauseButton.SetActive(true);
        
        //start the game
        Time.timeScale = 1;
    }
    
    //go to the main menu
    public void MainMenu()
    {
        // Load the game scene
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
        
        //dont destroy the music
        DontDestroyOnLoad(GameObject.Find("Music"));
    }
    
    //restart the game
    public void RestartGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainGame");
        
        //dont destroy the music
        DontDestroyOnLoad(GameObject.Find("Music"));
    }
    
    //disable the music
    public void DisableMusic()
    {
        //disable the music
        GameObject.Find("Music").GetComponent<AudioSource>().mute = true;
        
        //change the music icon
        musicOn.SetEnabled(false);
        musicOff.SetEnabled(true);
        
        musicEnabled = false;
        
    }
    
    //enable the music
    public void EnableMusic()
    {
        //enable the music
        GameObject.Find("Music").GetComponent<AudioSource>().mute = false;
        
        //change the music icon
        musicOn.SetEnabled(true);
        musicOff.SetEnabled(false);
        
        musicEnabled = true;
    }
    
    public void EnableSFX()
    {
        
        //TODO find a way to enable the sfx
        //enable the music
        GameObject.Find("SFX").GetComponent<AudioSource>().mute = false;
        
        //change the music icon
        sfxOn.SetEnabled(true);
        sfxOff.SetEnabled(false);
        
        sfxEnabled = true;
    }
    
    public void DisableSFX()
    {
        //TODO find a way to disable the sfx
        //disable the music
        GameObject.Find("SFX").GetComponent<AudioSource>().mute = true;
        
        //change the music icon
        sfxOn.SetEnabled(false);
        sfxOff.SetEnabled(true);
        
        sfxEnabled = false;
    }
}
