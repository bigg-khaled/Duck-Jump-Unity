using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ChallengeHandler : MonoBehaviour
{
    
    public Challenge currentChallenge;
    private float timeLeft;
    public float pauseBeforeNextChallenge = 2f; 
    private bool isChallengeActive;
    private bool isChallengeCompleted;
    public TextMeshProUGUI challengeText;

    private void Start()
    { 
        // create a new challenge.
        currentChallenge.CreateChallenge();
        timeLeft = currentChallenge.timeLimit;
        isChallengeActive = true;
        isChallengeCompleted = false;
        
    }
    
    private void FixedUpdate()
    {
       //get challenge type to display on screen and start challenge
       if (isChallengeActive)
       { 
           currentChallenge.StartChallenge();
           // Debug.Log("Challenge Started: \n" + currentChallenge.challengeText);
           challengeText.text = currentChallenge.challengeText;
           //start timer
           StartCoroutine(ChallengeTimer());

           if(currentChallenge.GetChallengeStatus() == Challenge.ChallengeStatus.COMPLETED)
           {
               isChallengeCompleted = true;
               isChallengeActive = false;
               Debug.Log("Challenge Completed");
               //stop challenge timer coroutine
               StopCoroutine(ChallengeTimer());
                //run a 2 second delay
               challengeText.text = "GO DUCKY!";
               StartCoroutine(NextChallengeDelay());
                
                
           }

           
       }
    }

    IEnumerator ChallengeTimer()
    {
        yield return new WaitForSeconds(timeLeft);
        if (!isChallengeCompleted)
        {
            //Game Over
            currentChallenge.FailChallenge();
            isChallengeActive = false;
            isChallengeCompleted = false;
            challengeText.text = "WHAT THE DUCK?!";
            
            //TODO make duck fall off screen
            
            //TODO show Game over menu 
        }
    }
    
    IEnumerator NextChallengeDelay()
    {
        yield return new WaitForSeconds(pauseBeforeNextChallenge);
        isChallengeCompleted = false;
        isChallengeActive = true;
        currentChallenge.CreateChallenge();
        timeLeft = currentChallenge.timeLimit;
    }
    
}