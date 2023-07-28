using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;


public class AudioControl : MonoBehaviour
{
    public GameObject audioOn;
    public GameObject audioOff;
    private bool audioEnabled;
    public AudioSource audioSource;


    private void Start()
    {
        
    }

    private void Awake()
    {

        //if audio source is not set, find it
        if (audioSource == null && GameObject.Find("Music") != null)
        {
            audioSource = GameObject.Find("Music").GetComponent<AudioSource>();
            
            //get enabled from player prefs and set the audio source mute accordingly
            if (PlayerPrefs.GetInt("MusicAudioEnabled", 1) == 1)
            {
                audioSource.mute = false;
                audioEnabled = true;
            }
            else
            {
                audioSource.mute = true;
                audioEnabled = false;
            }
        }
        
        //if audio source is sfx
        if (audioSource == null && GameObject.Find("SFX") != null)
        {
            audioSource = GameObject.Find("SFX").GetComponent<AudioSource>();
            
            //get enabled from player prefs and set the audio source mute accordingly
            if (PlayerPrefs.GetInt("SFXAudioEnabled", 1) == 1)
            {
                audioSource.mute = false;
                audioEnabled = true;
            }
            else
            {
                audioSource.mute = true;
                audioEnabled = false;
            }
        }

        if (audioEnabled)
        {
            //enable the audio
            audioSource.mute = false;

            //change the audio icon
            audioOn.GetComponent<Image>().enabled = true;
            audioOff.GetComponent<Image>().enabled = false;

            audioEnabled = true;
        }
        else
        {
            //disable the audio
            audioSource.mute = true;

            //change the audio icon
            audioOn.GetComponent<Image>().enabled = false;
            audioOff.GetComponent<Image>().enabled = true;

            audioEnabled = false;
        }

    }

    //enable the audio
    public void EnableAudio()
    {
        
        if (!audioEnabled)
        {
            //enable the audio
            audioSource.mute = false;

            //change the audio icon
            audioOn.GetComponent<Image>().enabled = true;
            audioOff.GetComponent<Image>().enabled = false;
            
            audioEnabled = true;
            
            //save the audio enabled state
            if (audioSource.gameObject.name == "Music")
            {
                PlayerPrefs.SetInt("MusicAudioEnabled", 1);
            }
            else
            {
                PlayerPrefs.SetInt("SFXAudioEnabled", 1);
            }


        }
        else
        {
            //disable the audio
            audioSource.mute = true;

            //change the audio icon
            audioOn.GetComponent<Image>().enabled = false;
            audioOff.GetComponent<Image>().enabled = true;

            audioEnabled = false;
            
            
            //save the audio enabled state
            if (audioSource.gameObject.name == "Music")
            {
                PlayerPrefs.SetInt("MusicAudioEnabled", 0);
            }
            else
            {
                PlayerPrefs.SetInt("SFXAudioEnabled", 0);
            }
        }
    }
}
