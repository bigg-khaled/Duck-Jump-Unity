using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChallengeHandler : MonoBehaviour
{
    
    public Challenge currentChallenge;
    private float timeLeft;
    private bool isChallengeActive;
    private bool isChallengeCompleted;
    

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
           
           //start timer
           StartCoroutine(ChallengeTimer());
           
           if(currentChallenge.GetChallengeStatus() == Challenge.ChallengeStatus.COMPLETED)
           {
               isChallengeCompleted = true;
               isChallengeActive = false;
               Debug.Log("Challenge Completed");
           }
       }
       
       if(!isChallengeActive && isChallengeCompleted)
       {
           //Create new challenge
           currentChallenge.StartChallenge();
       }
       
       


    }

    IEnumerator ChallengeTimer()
    {
        yield return new WaitForSeconds(timeLeft);
        if (!isChallengeCompleted)
        {
            //Game Over
            currentChallenge.FailChallenge();
        }
    }
    
}