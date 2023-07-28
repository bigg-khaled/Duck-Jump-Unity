using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PauseGame : MonoBehaviour
{
    public GameObject pauseMenu;
    public TextMeshProUGUI challengeText;
 
    
    public void PauseGameLevel()
    {

        //stop the game
        Time.timeScale = 0;
        
        //hide challenge text
        challengeText.gameObject.SetActive(false);
        
        
        // hide the pause button
        gameObject.SetActive(false);
        
        // show the pause menu
        pauseMenu.SetActive(true);
        
    }
}
