using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChallengeHandler : MonoBehaviour
{
    // This is a list of all the challenges that will be available in the game.
    // get a random challenge from this list and assign it to the currentChallenge variable.
[SerializeField] private List<Challenge> challenges;
private Challenge currentChallenge;
private float timeLeft;
private bool isChallengeActive;
private bool isChallengeCompleted;
private int score;


}
