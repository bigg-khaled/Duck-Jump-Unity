using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RateGame : MonoBehaviour
{
    public void OnRateButtonClick()
    {
        #if UNITY_ANDROID
                Application.OpenURL(string.Format("market://details?id=" + Application.identifier));
        #elif UNITY_IPHONE
                Application.OpenURL("itms-apps://itunes.apple.com/app/" + Application.identifier);
        #endif
    }
}