using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ChallengeHandler : MonoBehaviour
{
    
    public Challenge currentChallenge;
    private float timeLeft;
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
        
        // Debug.Log(currentChallenge.GetChallengeType() + " " + isChallengeActive + " " + isChallengeActive);
    }
    
    private void FixedUpdate()
    {
       //get challenge type to display on screen and start challenge
       if (isChallengeActive)
       { 
           currentChallenge.StartChallenge();
           Debug.Log("Challenge Started: \n" + currentChallenge.challengeText);
           challengeText.text = currentChallenge.challengeText;
           //start timer
           StartCoroutine(ChallengeTimer());
           
           if(currentChallenge.GetChallengeStatus() == Challenge.ChallengeStatus.COMPLETED)
           {
               isChallengeCompleted = true;
               isChallengeActive = false;
               Debug.Log("Challenge Completed");
           }

           
       }

       if(!isChallengeActive && isChallengeCompleted && currentChallenge.GetChallengeStatus() == Challenge.ChallengeStatus.COMPLETED)
       {
           //Create new challenge
           isChallengeActive = true;
           currentChallenge.CreateChallenge();
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
            challengeText.text = "Failed";
        }
    }
    
}