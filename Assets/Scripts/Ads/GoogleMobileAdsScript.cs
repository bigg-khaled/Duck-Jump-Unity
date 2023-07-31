using System.Collections;
using System.Collections.Generic;
using GoogleMobileAds.Api;
using UnityEngine;

public class GoogleMobileAdsScript : MonoBehaviour
{
    public void Start()
    {
        // Initialize the Google Mobile Ads SDK.
        MobileAds.Initialize(initStatus => { });
    }
}
