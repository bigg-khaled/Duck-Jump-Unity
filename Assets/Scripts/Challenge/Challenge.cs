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
        MIND_THE_GAP,
        STAND_UP,
        HIT_SEAGULL,
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
    public ChallengeType challengeType;
    private ChallengeStatus challengeStatus;
    public int amount;
    public float timeLimit;
    public int score;
    public int scoreMultiplier;
    public GameObject player;
    public string challengeText;

    //variables for GO_BACKWARD challenge
    private bool startCounting = false;
    private int amountReached = 0;
    [NonSerialized]public float startPoint;

    //variables for standing up
    private float timeAtZZero = 0f;
    private bool challengeCompleted = false;

    //variables for mind the gap
    [NonSerialized] public float startGap = 0f;



    private void Start()
    {
        //player = GameObject.FindWithTag("Player");
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
            case ChallengeType.MIND_THE_GAP:
                StartMindTheGapChallenge();
                break;
            case ChallengeType.STAND_UP:
                StartStandUpChallenge();
                break;
            case ChallengeType.HIT_SEAGULL:
                StartSeagullChallenge();
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
        
        challengeType = (ChallengeType)UnityEngine.Random.Range(0, 8);
        //challengeType = ChallengeType.HIT_SEAGULL;
        //TODO: make sure the first challenge isn't a height challenge cause the duck falls from above and it counts that
        //TODO make challenge counter that represents the number of challenges completed
        //if challenge completed == 0, don't make it a height challenge
        //after completing challenge, add 1 to challenge completed
        
        
        print("Current challenge: " + challengeType);
        switch (challengeType)
        {
            case ChallengeType.FRONTFLIP:
                amount = UnityEngine.Random.Range(1, 2);
                //reset backflip count
                player.GetComponent<DuckMovement>().frontflipCount = 0;
                timeLimit = 15;
                score = 100;
                scoreMultiplier = 1;
                challengeText = "Perform " + amount + " front flips" + " in " + timeLimit + " seconds";
                break;
            case ChallengeType.REACH_HEIGHT:
                amount = UnityEngine.Random.Range(6, 10);
                timeLimit = 15;
                score = 100;
                scoreMultiplier = 1;
                challengeText = "Reach " + amount + " meters high" + " in " + timeLimit + " seconds";
                break;
            case ChallengeType.HIT_TARGET:
                //reset target hit
                player.GetComponent<DuckMovement>().isTargetHit = false;
                timeLimit = 30;
                score = 100;
                scoreMultiplier = 1;
                challengeText = "Hit the egg in " + timeLimit + " seconds";
                break;
            case ChallengeType.REACH_SPEED:
                amount = UnityEngine.Random.Range(3, 6);
                timeLimit = 15;
                score = 100;
                scoreMultiplier = 1;
                challengeText = "Reach speed of " + amount + " m/s" + " in " + timeLimit + " seconds";

                break;
            case ChallengeType.GO_BACKWARD:
                amount = UnityEngine.Random.Range(5, 10);
                timeLimit = 15;
                score = 100;
                scoreMultiplier = 1;
                challengeText = "Go backward " + amount + " pixels" + " in " + timeLimit + " seconds";
                break;
            case ChallengeType.MIND_THE_GAP:
                timeLimit = 15;
                score = 100;
                scoreMultiplier = 1;
                challengeText = "Mind the gap in " + timeLimit + " seconds";
                break;
            case ChallengeType.STAND_UP:
                timeLimit = 20;
                score = 100;
                scoreMultiplier = 1;
                challengeText = "Stand up perfectly in " + timeLimit + " seconds";
                break;
            case ChallengeType.HIT_SEAGULL:
                player.GetComponent<DuckMovement>().isTargetHit = false;
                timeLimit = 30;
                score = 100;
                scoreMultiplier = 1;
                challengeText = "Hit the seagull in " + timeLimit + " seconds";
                break;

        }
    }
    
    private void StartFrontflipChallenge()
    {
        if (player.GetComponent<DuckMovement>().frontflipCount >= amount)
        {
            //print(player.GetComponent<DuckMovement>().frontflipCount);
            CompleteChallenge();
            player.GetComponent<DuckMovement>().frontflipCount = 0;
        }
    }
    
    private void StartReachHeightChallenge()
    {
        if (player.transform.position.y >= (float) amount/5)
        {
            //print("Height left: " + (amount / 3 - player.transform.position.y));
            CompleteChallenge();
        }
    }
    
    private void StartHitTargetChallenge()
    {
        //if there is no target near player, create one in front of player
        //if (target == null)
        //{
        //    var newTraget = Instantiate(target, new Vector3(player.transform.position.x + 5, player.transform.position.y + 10, player.transform.position.z), Quaternion.identity);
        //}

        if (player.GetComponent<DuckMovement>().isTargetHit)
        {
            CompleteChallenge();
        }
    }
    
    private void StartReachSpeedChallenge()
    {
        //print("speed: " + player.GetComponent<Rigidbody2D>().velocity.x);
        if (player.GetComponent<Rigidbody2D>().velocity.x >= amount)
        {
            CompleteChallenge();
        }
    }

    private void StartGoBackwardChallenge()
    {
        amount = amount / 2;
        //if player is moving forward, don't start counting and reset the amountReached to zero
        if((player.GetComponent<Rigidbody2D>().velocity.x > 0.1f))
        {
            startCounting = false;
            amountReached = 0;
        }
        //if the player starts moving backwards, their startpoint is recorded and this function starts counting
        else if((player.GetComponent<Rigidbody2D>().velocity.x < 0) && (startCounting == false))
        {
            startPoint = player.transform.position.x;
            startCounting = true;
        }
        //calculates the differece between the startpoint recorded and how many pixels backwards the player has moved
        //Todo: convert x -> pixels somehow or calculate a doable multiplyer 
        if (startCounting)
        {
            amountReached = Mathf.Abs((int)startPoint - (int)player.transform.position.x);
            if(amountReached >= amount)
            {
                startCounting = false;
                CompleteChallenge();
                amountReached = 0;
            }
        }
    }

    private void StartMindTheGapChallenge()
    {
        
        if((player.transform.position.x > startGap)&& startGap != 0)
        {
            print(startGap);
            CompleteChallenge();
            startGap = 0;
        }
    }

    private void StartStandUpChallenge()
    {
        // Check if the player's z position is 0
        if ((int)player.GetComponent<DuckMovement>().rb.rotation == 0 && player.GetComponent<DuckMovement>().isGrounded)
        {
            //print("Z: "+ player.transform.rotation.z);
            // If it's the first time the player's z position is 0, record the time
            if (!challengeCompleted)
            {
                timeAtZZero = Time.time;
                challengeCompleted = true;
            }

            // Check if the player's z position has been 0 for 2 seconds
            if (Time.time - timeAtZZero >= 2f)
            {
                CompleteChallenge();
                timeAtZZero = 0f;
                challengeCompleted = false;
            }
        }
        else
        {
            // Reset the challenge completion status if the player's z position is not 0
            challengeCompleted = false;
        }
    }

    private void StartSeagullChallenge()
    {
        if (player.GetComponent<DuckMovement>().isTargetHit)
        {
            CompleteChallenge();
        }
    }
}
