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
    public GameObject interstitialAd;
    private AudioSource audioSource;
    public AudioClip[] challengeCompletedSFX;
    public AudioClip[] challengeFailedSFX;
    public GameObject reviveMenu;
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

        if (currentChallenge.GetChallengeStatus() == Challenge.ChallengeStatus.FAILED)
        {
            FailChallenge();
        }
    }

    IEnumerator ChallengeTimer()
    {
        float initialYPos = challengeText.transform.position.y;
        float targetYPos = duck.transform.position.y; // Adjust this to the actual position you want to approach

        float startTime = Time.time;
        float elapsedTime = 0f;
        float moveDuration = timeLeft;

        while (elapsedTime < moveDuration)
        {
            float ratio = Mathf.Clamp01(elapsedTime / moveDuration); // Calculate the ratio
            float newYPos = Mathf.Lerp(initialYPos, targetYPos, ratio);

            Vector3 newPos = new Vector3(challengeText.transform.position.x, newYPos, challengeText.transform.position.z);
            challengeText.transform.position = newPos;

            elapsedTime = Time.time - startTime;
            yield return null;
        }

        if (!isChallengeCompleted)
        {
            // Game Over
            currentChallenge.FailChallenge();
            FailChallenge();

            if (!isPlayed)
            {
                isPlayed = true;
                // Play challenge failed SFX
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

        if (challengeScore > PlayerPrefs.GetInt("HighScore", 0))
        {
            PlayerPrefs.SetInt("HighScore", challengeScore);
        }

        HiScore.text = PlayerPrefs.GetInt("HighScore", 0).ToString();

        Bread.text = "+" + challengeScore.ToString();
        if (addBred)
        {
            PlayerPrefs.SetInt("Bread", PlayerPrefs.GetInt("Bread", 0) + challengeScore);
            addBred = false;
        }


        print("Bread: " + PlayerPrefs.GetInt("Bread", 0));

        //if lost 3 times show interstitial ad
        //TODO Bug! every loss shows an ad
        PlayerPrefs.SetInt("InterstitialAd", PlayerPrefs.GetInt("InterstitialAd", 0) + 1);
        if (PlayerPrefs.GetInt("InterstitialAd", 0) >= 3)
        {
            PlayerPrefs.SetInt("InterstitialAd", 0);
            interstitialAd.SetActive(true);
        }
        
        //if revive menu is not active show game over, else if active hide game over
        if (!reviveMenu.activeSelf)
        {
            gameOverScreen.gameObject.SetActive(true);
            challengeText.gameObject.SetActive(false);
        }
        else if(reviveMenu.activeSelf)
        {
            gameOverScreen.gameObject.SetActive(false);
            challengeText.gameObject.SetActive(false);
        }
    }

    public void ResetChallenge()
    {
        //reset challenge text position
        challengeText.transform.position = new Vector3(challengeText.transform.position.x,
            intialChallengeTextYPos, challengeText.transform.position.z);

        isChallengeCompleted = false;
        isChallengeActive = true;
        currentChallenge.CreateChallenge();
        timeLeft = currentChallenge.timeLimit;
        challengeText.gameObject.SetActive(true);
        gameOverScreen.gameObject.SetActive(false);
        duck.GetComponent<DuckMovement>().enabled = true;
        camera.GetComponent<CinemachineVirtualCamera>().Follow = duck.transform;
        duck.GetComponent<DuckMovement>().ResetDuck();
        isPlayed = false;
        gameObject.SetActive(false);
    }
}
