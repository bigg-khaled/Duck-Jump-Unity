using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Challenge : ScriptableObject
{
    enum ChallengeType
    {
        BACKFLIP,
        REACH_HEIGHT,
        HIT_TARGET,
        REACH_SPEED,
    };
    
    [SerializeField] private ChallengeType challengeType;
    public int amount;
    public float timeLimit;
    public int score;
    public int scoreMultiplier;
    public bool isCompleted;
}
