using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class CheckMusicPref : MonoBehaviour
{
    private void Awake()
    {
        //if music, check if audio is enabled
        if (PlayerPrefs.GetInt("MusicAudioEnabled", 1) == 1)
        {
            //if enabled, show the audio on button
            gameObject.GetComponent<AudioSource>().mute = false;
            
            //change the audio icon
            // gameObject.transform.GetChild(0).GetComponent<Image>().enabled = true;
            // gameObject.transform.GetChild(1).GetComponent<Image>().enabled = false;
        }
        else
        {
            //if disabled, show the audio off button
            gameObject.GetComponent<AudioSource>().mute = true;
            
            //change the audio icon
            // gameObject.transform.GetChild(0).GetComponent<Image>().enabled = false;
            // gameObject.transform.GetChild(1).GetComponent<Image>().enabled = true;
        }
    }
}
