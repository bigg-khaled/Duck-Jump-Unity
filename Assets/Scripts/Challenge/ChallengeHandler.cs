using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
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
    private float intialChallengeTextYPos;
    public GameObject duck;
    public GameObject camera;
    public Canvas gameOverScreen;

    private void Start()
    {
        // create a new challenge.
        currentChallenge.CreateChallenge();
        timeLeft = currentChallenge.timeLimit;
        isChallengeActive = true;
        isChallengeCompleted = false;
        intialChallengeTextYPos = challengeText.transform.position.y;
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

            if (currentChallenge.GetChallengeStatus() == Challenge.ChallengeStatus.COMPLETED)
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
        //TODO make challenge text show time left by slowly descending to player y position
        //move challenge text to player y position with ratio to time left
        challengeText.transform.position = new Vector3(challengeText.transform.position.x,
            challengeText.transform.position.y - (timeLeft / currentChallenge.timeLimit),
            challengeText.transform.position.z);

        yield return new WaitForSeconds(timeLeft);

        if (!isChallengeCompleted)
        {
            //Game Over
            currentChallenge.FailChallenge();
            isChallengeActive = false;
            isChallengeCompleted = false;
            challengeText.text = "WHAT THE DUCK?!";

            //throw duck up for dramatic effect
            duck.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 10);
            //stop duck from moving
            duck.GetComponent<DuckMovement>().enabled = false;
            //stop camera from moving
            camera.GetComponent<CinemachineVirtualCamera>().Follow = null;

            //TODO show Game over menu 
            gameOverScreen.gameObject.SetActive(true);
            //have UI button for try again
            //show score
            //show high score
            //go to settings
            //go to main menu
        }
    }

    IEnumerator NextChallengeDelay()
    {
        yield return new WaitForSeconds(pauseBeforeNextChallenge);
        
        //reset challenge text position
        challengeText.transform.position = new Vector3(challengeText.transform.position.x,
            intialChallengeTextYPos, challengeText.transform.position.z);
        
        isChallengeCompleted = false;
        isChallengeActive = true;
        currentChallenge.CreateChallenge();
        timeLeft = currentChallenge.timeLimit;
    }
}