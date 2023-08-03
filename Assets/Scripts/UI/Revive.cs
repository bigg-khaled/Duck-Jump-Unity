using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Revive : MonoBehaviour
{
    public int price = 100;
    public TextMeshProUGUI priceText;
    public GameObject duck;
    public ChallengeHandler challengeHandler;
    public GameObject reviveMenu;
    public RewardedGameAd rewardedGameAd;
    
    public void ReviveDuck()
    {
        //hide revive menu
        reviveMenu.SetActive(false);
        
        //reset duck position
        duck.transform.position = new Vector3(0, 0, 0);
        
        //reset duck rotation
        duck.transform.rotation = Quaternion.Euler(0, 0, 0);
        
        //reset duck velocity
        duck.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        
        //reset duck angular velocity
        duck.GetComponent<Rigidbody2D>().angularVelocity = 0;
        
        //reset duck rotation
        duck.transform.rotation = Quaternion.Euler(0, 0, 0);
        
        //reset duck rotation
        duck.transform.rotation = Quaternion.Euler(0, 0, 0);
        
        //reset duck rotation
        duck.transform.rotation = Quaternion.Euler(0, 0, 0);
        
        //reset duck rotation
        duck.transform.rotation = Quaternion.Euler(0, 0, 0);
        
        //reset duck rotation
        duck.transform.rotation = Quaternion.Euler(0, 0, 0);
        
        //reset duck rotation
        duck.transform.rotation = Quaternion.Euler(0, 0, 0);
        
        //reset duck rotation
        duck.transform.rotation = Quaternion.Euler(0, 0, 0);
        
        //reset duck rotation
        duck.transform.rotation = Quaternion.Euler(0, 0, 0);
    }
    
    public void ResetChallenge()
    {
        //TODO
        //reset challenge
        // challengeHandler.ResetChallenge();
    }

    public void PayWithBread()
    {
        
    }
    
    public void WatchAd()
    {
        //show ad
        rewardedGameAd.enabled = true;
    }

}
