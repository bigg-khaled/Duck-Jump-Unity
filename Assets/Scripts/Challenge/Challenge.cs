using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Challenge : ScriptableObject
{

    public enum ChallengeType
    {
        BACKFLIP,
        REACH_HEIGHT,
        HIT_TARGET,
        REACH_SPEED,
    };

    public enum ChallnegeStatus
    {
        ON_GOING,
        COMPLETED,
        FAILED,
    };
    
    //challenges get harder, score multiplier increases
    //player momentum increases, score multiplier increases

    //pick random challenge type from enum
    private ChallengeType challengeType;
    private ChallnegeStatus challengeStatus;
    public int amount;
    public float timeLimit;
    public GameObject target;
    public int score;
    public int scoreMultiplier;
    private GameObject player;

    //get challenge type
    public ChallengeType GetChallengeType()
    {
        return challengeType;
    }
    
    public void CompleteChallenge()
    {
        challengeStatus = ChallnegeStatus.COMPLETED;
    }
    
    public void FailChallenge()
    {
        challengeStatus = ChallnegeStatus.FAILED;
    }
    
    public void StartChallenge()
    {
        challengeStatus = ChallnegeStatus.ON_GOING;
        player = GameObject.FindWithTag("Player");

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
                amount = UnityEngine.Random.Range(1, 4);
                timeLimit = 5;
                score = 100;
                scoreMultiplier = 1;
                break;
            case ChallengeType.REACH_HEIGHT:
                amount = UnityEngine.Random.Range(4, 20);
                timeLimit = 5;
                score = 100;
                scoreMultiplier = 1;
                break;
            case ChallengeType.HIT_TARGET:
                amount = 0;
                timeLimit = UnityEngine.Random.Range(15,30);
                score = 100;
                scoreMultiplier = 1;
                break;
            case ChallengeType.REACH_SPEED:
                amount = UnityEngine.Random.Range(1, 4);
                timeLimit = 5;
                score = 100;
                scoreMultiplier = 1;
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
