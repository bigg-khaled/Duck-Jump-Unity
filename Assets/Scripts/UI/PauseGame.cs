using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PauseGame : MonoBehaviour
{
    public GameObject pauseMenu;
    public TextMeshProUGUI challengeText;
    public TextMeshProUGUI tapToStartText;
    
    public void PauseGameLevel()
    {

        //stop the game
        Time.timeScale = 0;
        
        //hide challenge text
        challengeText.gameObject.SetActive(false);
        
        //hide tap to start text
        tapToStartText.gameObject.SetActive(false);

        // hide the pause button
        gameObject.SetActive(false);
        
        // show the pause menu
        pauseMenu.SetActive(true);
        
    }
}
