using System.Collections;
using System.Collections.Generic;
using GoogleMobileAds.Api;
using UnityEngine;

public class InterstitialGameAd : MonoBehaviour
{
    private InterstitialAd interstitialAd;

    
    //TODO change to real ad unit id
    #if UNITY_ANDROID
        string adUnitId = "ca-app-pub-1056016147843179/1851571161";
    #elif UNITY_IPHONE
        string adUnitId = "ca-app-pub-3940256099942544/4411468910";
    #else
        string adUnitId = "unexpected_platform";
    #endif
    
    public void Start()
    {
        MobileAds.Initialize(initStatus => { });
        
        // this.RequestInterstitial();
    }
    
    // private void RequestInterstitial()
    // {
    //     this.interstitialAd = new InterstitialAd(adUnitId);
    //     
    //     AdRequest request = new AdRequest.Builder().Build();
    //     
    //     this.interstitialAd.LoadAd(request);
    // }
}