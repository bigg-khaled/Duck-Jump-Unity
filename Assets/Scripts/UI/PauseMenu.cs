using System;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;
using Image = UnityEngine.UI.Image;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenu;
    public GameObject pauseButton;
    public TextMeshProUGUI challengeText;
    public TextMeshProUGUI tapToStartText;
    public GameObject interstitialGameAd;

    private void Start()
    {
        //if game has been restarted 3 times, show the ad
        if (PlayerPrefs.GetInt("RestartCount", 0) >= 3)
        {
            PlayerPrefs.SetInt("RestartCount", 0);
            interstitialGameAd.SetActive(true);
        }
    }

    private void Awake()
    {
        Time.timeScale = 0;
    }

    public void ResumeGame()
    {
        // hide the pause menu
        pauseMenu.SetActive(false);

        // show the pause button
        pauseButton.SetActive(true);

        //show challenge text
        challengeText.gameObject.SetActive(true);
        
        //show tap to start text
        tapToStartText.gameObject.SetActive(true);

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
        
        //dont destroy the sfx
        DontDestroyOnLoad(GameObject.Find("SFX"));

        //start the game
        Time.timeScale = 1;
    }

    //restart the game
    public void RestartGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainGame");
        
        //count the number of times the game has been restarted
        PlayerPrefs.SetInt("RestartCount", PlayerPrefs.GetInt("RestartCount", 0) + 1);
        
        //dont destroy the music
        DontDestroyOnLoad(GameObject.FindWithTag("Music"));
        
        //dont destroy the sfx
        DontDestroyOnLoad(GameObject.FindWithTag("SFX"));

        //start the game
        Time.timeScale = 1;
    }
}