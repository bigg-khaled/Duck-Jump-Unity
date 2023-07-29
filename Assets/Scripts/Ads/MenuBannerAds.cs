using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuBannerAds : MonoBehaviour
{
    
    LevelPlayAds levelPlayAds;
    // Start is called before the first frame update
    void Start()
    {
        levelPlayAds.LoadTopBanner();
    }
    
}
