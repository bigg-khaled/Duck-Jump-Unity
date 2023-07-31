using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;

public class BannerGameAd : MonoBehaviour
{
    private BannerView bannerView;

    private void Start()
    {
        MobileAds.Initialize(initStatus => { });
        
        this.RequestBanner();
    }
    
    private void RequestBanner()
    {
        //TODO change to real ad unit id

        #if UNITY_ANDROID
            string adUnitId = "ca-app-pub-1056016147843179/1851571161";
        #elif UNITY_IPHONE
            string adUnitId = "ca-app-pub-3940256099942544/2934735716";
        #else
            string adUnitId = "unexpected_platform";
        #endif
        
        this.bannerView = new BannerView(adUnitId, AdSize.Banner, AdPosition.Top);
        
        AdRequest request = new AdRequest.Builder().Build();
        
        this.bannerView.LoadAd(request);
    }
}
