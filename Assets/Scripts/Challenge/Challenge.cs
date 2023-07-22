using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Challenge : MonoBehaviour  
{

    public enum ChallengeType
    {
        FRONTFLIP,
        REACH_HEIGHT,
        HIT_TARGET,
        REACH_SPEED,
        GO_BACKWARD,
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

    //variables for GO_BACKWARD challenge
    bool startCounting = false;
    int amountReached = 0;
    float startPoint;



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
            case ChallengeType.FRONTFLIP:
                //check if player has done a backflip
                StartFrontflipChallenge();
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
            case ChallengeType.GO_BACKWARD:
                //check if player has moved backwards a certain amount of pixels
                StartGoBackwardChallenge();
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
        
        challengeType = (ChallengeType)UnityEngine.Random.Range(0, 4);
        switch (challengeType)
        {
            case ChallengeType.FRONTFLIP:
                amount = UnityEngine.Random.Range(1, 2);
                //reset backflip count
                player.GetComponent<DuckMovement>().frontflipCount = 0;
                timeLimit = 10;
                score = 100;
                scoreMultiplier = 1;
                challengeText = "Perform " + amount + " front flips" + " in " + timeLimit + " seconds";
                break;
            case ChallengeType.REACH_HEIGHT:
                amount = UnityEngine.Random.Range(4, 10);
                timeLimit = 10;
                score = 100;
                scoreMultiplier = 1;
                challengeText = "Reach " + amount + " meters high" + " in " + timeLimit + " seconds";

                break;
            case ChallengeType.HIT_TARGET:
                //reset target hit
                player.GetComponent<DuckMovement>().isTargetHit = false;
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
            case ChallengeType.GO_BACKWARD:
                amount = UnityEngine.Random.Range(5, 10);
                timeLimit = 10;
                score = 100;
                scoreMultiplier = 1;
                challengeText = "Go backward " + amount + " pixels" + " in " + timeLimit + " seconds";
                break;
        }
    }
    
    private void StartFrontflipChallenge()
    {
        if (player.GetComponent<DuckMovement>().frontflipCount >= amount)
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
        //if there is no target near player, create one in front of player
        if (target == null)
        {
            var newTraget = Instantiate(target, new Vector3(player.transform.position.x + 5, player.transform.position.y + 10, player.transform.position.z), Quaternion.identity);
        }

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

    private void StartGoBackwardChallenge()
    {
        //if player is moving forward, don't start counting and reset the amountReached to zero
        if((player.GetComponent<Rigidbody2D>().velocity.x > 0.1f))
        {
            startCounting = false;
            amountReached = 0;
        }
        //if the player starts moving backwards, their startpoint is recorded and this function starts counting
        else if((player.GetComponent<Rigidbody2D>().velocity.x < 0) && (startCounting == false))
        {
            startPoint = player.AddComponent<Rigidbody2D>().transform.position.x;
            startCounting = true;
        }
        //calculates the differece between the startpoint recorded and how many pixels backwards the player has moved
        //Todo: convert x -> pixels somehow or calculate a doable multiplyer 
        if (startCounting)
        {
            amountReached = Mathf.Abs((int)startPoint - (int)player.AddComponent<Rigidbody2D>().transform.position.x);
            if(amountReached >= amount)
            {
                startCounting = false;
                CompleteChallenge();
                amountReached = 0;
            }
        }
    }

}
