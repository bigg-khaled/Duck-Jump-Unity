using System;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;
using Image = UnityEngine.UI.Image;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenu;
    public GameObject pauseButton;
    public GameObject musicOn;
    public GameObject musicOff;
    public GameObject sfxOn;
    public GameObject sfxOff;
    private bool musicEnabled;
    private bool sfxEnabled;

    public TextMeshProUGUI challengeText;
    public GameObject duck;
    public ChallengeHandler challengeHandler;

    // private void Awake()
    // {
    //     musicOn = GameObject.Find("MusicOnIcon").GetComponent<Image>();
    //     musicOff = GameObject.Find("MusicOffIcon").GetComponent<Image>();
    //     sfxOn = GameObject.Find("SFXOnIcon").GetComponent<Image>();
    //     sfxOff = GameObject.Find("SFXOffIcon").GetComponent<Image>();
    // }

    private void Start()
    {
        Time.timeScale = 1;
        musicEnabled = true;
        sfxEnabled = true;
    }

    public void ResumeGame()
    {
        // hide the pause menu
        pauseMenu.SetActive(false);

        // show the pause button
        pauseButton.SetActive(true);

        //show challenge text
        challengeText.gameObject.SetActive(true);

        //start the game
        Time.timeScale = 1;
    }

    //go to the main menu
    public void MainMenu()
    {
        // Load the game scene
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");

        //dont destroy the music
        //DontDestroyOnLoad(GameObject.Find("Music"));

        //start the game
        Time.timeScale = 1;
    }

    //restart the game
    public void RestartGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainGame");

        //dont destroy the music
        DontDestroyOnLoad(GameObject.FindWithTag("Music"));

        //start the game
        Time.timeScale = 1;
    }


    //enable the music
    public void EnableMusic()
    {
        if (!musicEnabled)
        {
            //enable the music
            GameObject.FindWithTag("Music").GetComponent<AudioSource>().mute = false;

            //change the music icon
            musicOn.GetComponent<Image>().enabled = true;
            musicOff.GetComponent<Image>().enabled = false;

            musicEnabled = true;
        }
        else
        {
            //disable the music
            GameObject.FindWithTag("Music").GetComponent<AudioSource>().mute = true;

            //change the music icon
            musicOn.GetComponent<Image>().enabled = false;
            musicOff.GetComponent<Image>().enabled = true;

            musicEnabled = false;
        }
    }

    public void EnableSFX()
    {
        if (!sfxEnabled)
        {
            //enable the music
            duck.GetComponent<AudioSource>().mute = false;
            challengeHandler.GetComponent<AudioSource>().mute = false;


            //change the music icon
            sfxOn.GetComponent<Image>().enabled = true;
            sfxOff.GetComponent<Image>().enabled = false;

            sfxEnabled = true;
        }
        else
        {
            //disable the music
            duck.GetComponent<AudioSource>().mute = true;
            challengeHandler.GetComponent<AudioSource>().mute = true;

            //change the music icon
            sfxOn.GetComponent<Image>().enabled = false;
            sfxOff.GetComponent<Image>().enabled = true;

            sfxEnabled = false;
        }
    }
}