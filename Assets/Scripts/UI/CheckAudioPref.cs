using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckAudioPref : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
     //check if audio is enabled
        if (PlayerPrefs.GetInt("MusicAudioEnabled", 1) == 1)
        {
            //if enabled, show the audio on button
            gameObject.GetComponent<AudioSource>().mute = false;
        }
        else
        {
            //if disabled, show the audio off button
            gameObject.GetComponent<AudioSource>().mute = true;
        }
    }
}
