using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class SFXControl : MonoBehaviour
{
    public GameObject SFXOn;
    public GameObject SFXOff;
    private bool SFXEnabled;
    public AudioSource audioSource;


    private void Awake()
    {
        //if audio source is sfx
     
            audioSource = GameObject.Find("SFX").GetComponent<AudioSource>();
            
            //get enabled from player prefs and set the audio source mute accordingly
            if (PlayerPrefs.GetInt("SFXAudioEnabled", 1) == 1)
            {
                audioSource.mute = false;
                SFXEnabled = true;
                
                //change the audio icon
                SFXOn.GetComponent<Image>().enabled = true;
                SFXOff.GetComponent<Image>().enabled = false;
                
            }
            else
            {
                audioSource.mute = true;
                SFXEnabled = false;
                
                //change the audio icon
                SFXOn.GetComponent<Image>().enabled = false;
                SFXOff.GetComponent<Image>().enabled = true;
            }
        
    }

    //enable the audio
    public void ToggleSFX()
    {
        
        if (!SFXEnabled)
        {
            //enable the audio
            audioSource.mute = false;

            //change the audio icon
            SFXOn.GetComponent<Image>().enabled = true;
            SFXOff.GetComponent<Image>().enabled = false;
            
            SFXEnabled = true;
            
            //save the audio enabled state
            PlayerPrefs.SetInt("SFXAudioEnabled", 1);


        }
        else
        {
            //disable the audio
            audioSource.mute = true;

            //change the audio icon
            SFXOn.GetComponent<Image>().enabled = false;
            SFXOff.GetComponent<Image>().enabled = true;

            SFXEnabled = false;
            
            
            //save the audio enabled state
            PlayerPrefs.SetInt("SFXAudioEnabled", 0);
        }
    }
}
