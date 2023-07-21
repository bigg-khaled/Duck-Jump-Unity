using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Challenge : MonoBehaviour  
{

    public enum ChallengeType
    {
        BACKFLIP,
        REACH_HEIGHT,
        HIT_TARGET,
        REACH_SPEED,
    };

    public enum ChallengeStatus
    {
        ON_GOING,
        COMPLETED,
        FAILED,
    };
    
    //challenges get harder, score multiplier increases
    //player momentum increases, score multiplier increases

    //pick random challenge type from enum
    private ChallengeType challengeType;
    private ChallengeStatus challengeStatus;
    public int amount;
    public float timeLimit;
    public GameObject target;
    public int score;
    public int scoreMultiplier;
    public GameObject player;
    public string challengeText;


    private void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

    //get challenge type
    public ChallengeType GetChallengeType()
    {
        return challengeType;
    }

    public ChallengeStatus GetChallengeStatus()
    {
        return challengeStatus;
    }
    
    public void CompleteChallenge()
    {
        challengeStatus = ChallengeStatus.COMPLETED;
    }
    
    public void FailChallenge()
    {
        challengeStatus = ChallengeStatus.FAILED;
    }
    
    public void StartChallenge()
    {
        challengeStatus = ChallengeStatus.ON_GOING;

        //preform challenge
        switch (challengeType)
        {
            case ChallengeType.BACKFLIP:
                //check if player has done a backflip
                StartBackflipChallenge();
                break;
            case ChallengeType.REACH_HEIGHT:
                //check if player has reached a certain height
                StartReachHeightChallenge();
                break;
            case ChallengeType.HIT_TARGET:
                //check if player has hit a target
                StartHitTargetChallenge();
                break;
            case ChallengeType.REACH_SPEED:
                //check if player has reached a certain speed
                StartReachSpeedChallenge();
                break;
        } 
        
        Debug.Log("Challenge Started: \n" + challengeText);   
    }

    //create random challenge
    public void CreateChallenge()
    {
        // get a random challenge from the list of challenges and assign it to the currentChallenge variable.
        // set the timeLeft variable to the timeLimit of the currentChallenge.
        // set the isChallengeActive variable to true.
        // set the isChallengeCompleted variable to false.
        
        //reset backflip count
        player.GetComponent<DuckMovement>().backflipCount = 0;
        
        //reset target hit
        player.GetComponent<DuckMovement>().isTargetHit = false;
        
        challengeType = (ChallengeType)UnityEngine.Random.Range(0, 4);
        switch (challengeType)
        {
            case ChallengeType.BACKFLIP:
                amount = UnityEngine.Random.Range(1, 2);
                timeLimit = 10;
                score = 100;
                scoreMultiplier = 1;
                challengeText = "Perform " + amount + " Backflips" + " in " + timeLimit + " seconds";
                break;
            case ChallengeType.REACH_HEIGHT:
                amount = UnityEngine.Random.Range(4, 20);
                timeLimit = 10;
                score = 100;
                scoreMultiplier = 1;
                challengeText = "Reach " + amount + " meters high " + " in " + timeLimit + " seconds";

                break;
            case ChallengeType.HIT_TARGET:
                amount = UnityEngine.Random.Range(1, 4);
                timeLimit = UnityEngine.Random.Range(10,15);
                score = 100;
                scoreMultiplier = 1;
                challengeText = "Hit " + target.name + " in " + timeLimit + " seconds";
                break;
            case ChallengeType.REACH_SPEED:
                amount = UnityEngine.Random.Range(1, 4);
                timeLimit = 10;
                score = 100;
                scoreMultiplier = 1;
                challengeText = "Reach speed of " + amount + " m/s" + " in " + timeLimit + " seconds";

                break;
        }
    }
    
    private void StartBackflipChallenge()
    {
        if (player.GetComponent<DuckMovement>().backflipCount >= amount)
        {
            CompleteChallenge();
        }
    }
    
    private void StartReachHeightChallenge()
    {
        if (player.transform.position.y >= amount)
        {
            CompleteChallenge();
        }
    }
    
    private void StartHitTargetChallenge()
    {
        //check if there is a target in front of the player
        

        //if there is not create one
        
        
        if (player.GetComponent<DuckMovement>().isTargetHit)
        {
            CompleteChallenge();
        }
    }
    
    private void StartReachSpeedChallenge()
    {
        if (player.GetComponent<Rigidbody2D>().velocity.x >= amount)
        {
            CompleteChallenge();
        }
    }
    
}
