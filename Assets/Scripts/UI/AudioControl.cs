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

        //Had to convert to bool since playerprefs dont do bool
        //Basically the variable is AudioSetting, and the 0 means that if nothing is set, it is 0 by default (False)
        audioSource.mute = Convert.ToBoolean(PlayerPrefs.GetInt("AudioSetting", 0));

        //Go to the function ta7t to see how to change it b2a

        //ur gonna have to make another one for audioenabled aw connect them cause I dont wanna mess with ur code
        

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

            //Using SetInt means that first prop is the variable, second is the number aw variable u wanna assign it
            PlayerPrefs.SetInt("AudioSetting", 0);

            //change the audio icon
            audioOn.GetComponent<Image>().enabled = true;
            audioOff.GetComponent<Image>().enabled = false;

            audioEnabled = true;
        }
        else
        {
            //disable the audio
            audioSource.mute = true;

            //same here but I set it to true
            PlayerPrefs.SetInt("AudioSetting", 1);

            //change the audio icon
            audioOn.GetComponent<Image>().enabled = false;
            audioOff.GetComponent<Image>().enabled = true;

            audioEnabled = false;
        }
    }
}
