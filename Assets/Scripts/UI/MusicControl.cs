using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class MusicControl : MonoBehaviour
{
    public GameObject MusicOn;
    public GameObject MusicOff;
    private bool MusicEnabled;
    public AudioSource audioSource;


    private void Awake()
    {
        //if audio source is sfx
       
            audioSource = GameObject.Find("Music").GetComponent<AudioSource>();
            
            //get enabled from player prefs and set the audio source mute accordingly
            if (PlayerPrefs.GetInt("MusicAudioEnabled", 1) == 1)
            {
                audioSource.mute = false;
                MusicEnabled = true;
                
                //change the audio icon
                MusicOn.GetComponent<Image>().enabled = true;
                MusicOff.GetComponent<Image>().enabled = false;
                
            }
            else
            {
                audioSource.mute = true;
                MusicEnabled = false;
                
                //change the audio icon
                MusicOn.GetComponent<Image>().enabled = false;
                MusicOff.GetComponent<Image>().enabled = true;
            }
        
    }

    //enable the audio
    public void ToggleMusic()
    {
        
        if (!MusicEnabled)
        {
            //enable the audio
            audioSource.mute = false;

            //change the audio icon
            MusicOn.GetComponent<Image>().enabled = true;
            MusicOff.GetComponent<Image>().enabled = false;
            
            MusicEnabled = true;
            
            //save the audio enabled state
            PlayerPrefs.SetInt("MusicAudioEnabled", 1);


        }
        else
        {
            //disable the audio
            audioSource.mute = true;

            //change the audio icon
            MusicOn.GetComponent<Image>().enabled = false;
            MusicOff.GetComponent<Image>().enabled = true;

            MusicEnabled = false;
            
            
            //save the audio enabled state
            PlayerPrefs.SetInt("MusicAudioEnabled", 0);
        }
    }
}
