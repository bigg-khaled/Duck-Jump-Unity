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
    public TextMeshProUGUI challengesCompleted;
    public TextMeshProUGUI FinalScoreValue;
    public TextMeshProUGUI HiScore;
    public TextMeshProUGUI Bread;
    private int challengeScore = 0;
    private float intialChallengeTextYPos;
    public GameObject duck;
    public GameObject camera;
    public Canvas gameOverScreen;
    private String[] challengeCompletedText;
    
    private AudioSource audioSource;
    public AudioClip[] challengeCompletedSFX;
    public AudioClip[] challengeFailedSFX;
    private bool isPlayed = false;

    //bred
    bool addBred;

    private void Awake()
    {
        //duck = GameObject.FindWithTag("Player");
        //wait till the player lands on the ground
        StartCoroutine(WaitForPlayerToLand());
        
        
        // create a new challenge.
        currentChallenge.CreateChallenge();
        timeLeft = currentChallenge.timeLimit;
        isChallengeActive = true;
        isChallengeCompleted = false;
        intialChallengeTextYPos = challengeText.transform.position.y;
        
        //read challenge completed text from file
        challengeCompletedText = System.IO.File.ReadAllLines(@"Assets\Scripts\Challenge\ChallengeCompletedText.txt");
        
        HiScore.text = PlayerPrefs.GetInt("HighScore", 0).ToString();
        
        //get sfx audio source
        audioSource = GameObject.Find("SFX").GetComponent<AudioSource>();
    }

    private void FixedUpdate()
    {
        //get challenge type to display on screen and start challenge
        if (isChallengeActive)
        {
            challengesCompleted.text = $"{challengeScore}";
            addBred = true;
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
                
                //play challenge completed SFX
                int randomSFX = UnityEngine.Random.Range(0, challengeCompletedSFX.Length);
                audioSource.PlayOneShot(challengeCompletedSFX[randomSFX]);
                //stop challenge timer coroutine
                StopAllCoroutines();
                //run a 2 second delay
                //get random challenge completed text
                int randomText = UnityEngine.Random.Range(0, challengeCompletedText.Length);
                challengeText.text = challengeCompletedText[randomText];
                ++challengeScore;
                //challengesCompleted.text = $"{challengeScore}";
                StartCoroutine(CreateChallenge());
            }
        }
        
        if(currentChallenge.GetChallengeStatus() == Challenge.ChallengeStatus.FAILED)
        {
            FailChallenge();
        }
    }

    IEnumerator ChallengeTimer()
    {
        //move challenge text to player y position with ratio to time left
        challengeText.transform.position = new Vector3(challengeText.transform.position.x,
            challengeText.transform.position.y - (timeLeft / currentChallenge.timeLimit),
            challengeText.transform.position.z);
        
        yield return new WaitForSeconds(timeLeft);

        if (!isChallengeCompleted)
        {
            //Game Over
            currentChallenge.FailChallenge();
            
            FailChallenge();
            
            
            if (!isPlayed)
            {
                isPlayed = true;
                //play challenge failed SFX
                int randomSFX = UnityEngine.Random.Range(0, challengeFailedSFX.Length);
                audioSource.PlayOneShot(challengeFailedSFX[randomSFX]);
            }
        }
        
    }

    IEnumerator WaitForPlayerToLand()
    {
        yield return new WaitUntil(() => duck.GetComponent<DuckMovement>().isGrounded);
        //create a new challenge
        
    }

    IEnumerator CreateChallenge()
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

    public void FailChallenge()
    {
        isChallengeActive = false;
        isChallengeCompleted = false;
        challengeText.text = "WHAT THE DUCK?!";
            
        //reset challenge text position
        challengeText.transform.position = new Vector3(challengeText.transform.position.x,
            intialChallengeTextYPos, challengeText.transform.position.z);

        //throw duck up for dramatic effect
        duck.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 10);
        //stop duck from moving
        duck.GetComponent<DuckMovement>().enabled = false;
        //stop camera from moving
        camera.GetComponent<CinemachineVirtualCamera>().Follow = null;

        FinalScoreValue.text = $"{challengeScore}";

        if(challengeScore > PlayerPrefs.GetInt("HighScore", 0))
        {
            PlayerPrefs.SetInt("HighScore", challengeScore);    
        }
        HiScore.text = PlayerPrefs.GetInt("HighScore", 0).ToString();

        Bread.text = challengeScore.ToString();
        if (addBred)
        { 
            PlayerPrefs.SetInt("Bread", PlayerPrefs.GetInt("Bread", 0) + challengeScore);
            addBred = false;
        }

        
        print("Bread: " + PlayerPrefs.GetInt("Bread", 0));

        gameOverScreen.gameObject.SetActive(true);
        challengeText.gameObject.SetActive(false);
    }
}