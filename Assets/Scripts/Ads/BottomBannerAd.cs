using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BottomBannerAd : MonoBehaviour
{
    public LevelPlayAds levelPlayAds;
    // Start is called before the first frame update
    void Start()
    {
        levelPlayAds.LoadBottomBanner();
    }


}
