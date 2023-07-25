using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PauseGame : MonoBehaviour
{
    public GameObject pauseMenu;
    public TextMeshProUGUI challengeText;
    private void Start()
    {
        Time.timeScale = 1;
    }
    
    public void PauseGameLevel()
    {

        //hide challenge text
        challengeText.gameObject.SetActive(false);
        
        
        // hide the pause button
        gameObject.SetActive(false);
        
        // show the pause menu
        pauseMenu.SetActive(true);
        
        //stop the game
        Time.timeScale = 0;
        
    }
}
