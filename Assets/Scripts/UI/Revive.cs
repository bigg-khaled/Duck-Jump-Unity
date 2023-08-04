using System;
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
    public Canvas gameOverScreen;
    public GameObject rewardedGameAdPrefab;
    
    public void ShowReviveMenu()
    {
        //show revive menu
        reviveMenu.SetActive(true);
        
        //hide game over screen
        gameOverScreen.gameObject.SetActive(false);
        
        //show price
        priceText.text = price.ToString();
    }

    public void PayWithBread()
    {
        rewardedGameAdPrefab.SetActive(false);
        print("pay with bread clicked");

        //get bread from player prefs, if not enough bread, show ad
        int bread = PlayerPrefs.GetInt("Bread", 0);
        
        if(bread < price)
        {
            //show ad
            rewardedGameAdPrefab.SetActive(true);
        }
        else
        {
            //deduct bread
            PlayerPrefs.SetInt("Bread", bread - price);
            
            //revive duck
            duck.SetActive(true);
            
            //hide revive menu
            reviveMenu.SetActive(false);
            
            //reset challenge
            challengeHandler.ResetChallenge();
        }


    }

    public void WatchAd()
    {
        print("watch ad clicked");
        rewardedGameAdPrefab.SetActive(false);

        
        duck.SetActive(true);

        //hide revive menu
        reviveMenu.SetActive(false);
        //show ad
        rewardedGameAdPrefab.SetActive(true);
        
    }
}
