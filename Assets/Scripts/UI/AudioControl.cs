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
        //TODO get enabled from player prefs and set the audio source mute accordingly... ya3ni if audio is enabled, set the audio source mute to false
    }

    private void Awake()
    {

        //if audio source is not set, find it
        if (audioSource == null && GameObject.Find("Music") != null)
        {
            audioSource = GameObject.Find("Music").GetComponent<AudioSource>();
        }

        if (!audioEnabled)
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
        
        audioOff.GetComponent<Image>().enabled = false;
        

    }

    //enable the audio
    public void EnableAudio()
    {
        print("clicked");
        
        if (!audioEnabled)
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
}
