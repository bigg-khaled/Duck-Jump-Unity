using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;


public class MusicAudioControl : MonoBehaviour
{
    [FormerlySerializedAs("musicOn")] public GameObject audioOn;
    [FormerlySerializedAs("musicOff")] public GameObject audioOff;
    private bool audioEnabled;
    public GameObject duck;
    public ChallengeHandler challengeHandler;

    
    private void Start()
    {
        //TODO make it enabled based on saved settings
        audioEnabled = true;
    }

    
    //enable the music
    public void EnableMusic()
    {
        if (!audioEnabled)
        {
            //enable the music
            GameObject.FindWithTag("Music").GetComponent<AudioSource>().mute = false;

            //change the music icon
            audioOn.GetComponent<Image>().enabled = true;
            audioOff.GetComponent<Image>().enabled = false;

            audioEnabled = true;
        }
        else
        {
            //disable the music
            GameObject.FindWithTag("Music").GetComponent<AudioSource>().mute = true;

            //change the music icon
            audioOn.GetComponent<Image>().enabled = false;
            audioOff.GetComponent<Image>().enabled = true;

            audioEnabled = false;
        }
    }
}
