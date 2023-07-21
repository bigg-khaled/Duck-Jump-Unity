using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChallengeHandler : MonoBehaviour
{
    
    private Challenge currentChallenge;
    private float timeLeft;
    private bool isChallengeActive;
    private bool isChallengeCompleted;
    

    private void Start()
    { 
        // create a new challenge.
        currentChallenge = ScriptableObject.CreateInstance<Challenge>();
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
           Debug.Log("Challenge Type: " + currentChallenge.GetChallengeType());
       }
       
       


    }
    
}