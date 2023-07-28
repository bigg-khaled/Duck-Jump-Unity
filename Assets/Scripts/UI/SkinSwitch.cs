using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkinSwitch : MonoBehaviour
{
    int skinChoice;

    private void Awake()
    {
        skinChoice = PlayerPrefs.GetInt("Skin", 0);
    }
    void Start()
    {
        skinChoice = PlayerPrefs.GetInt("Skin", 0);
    }


    void Update()
    {
        print(PlayerPrefs.GetInt("Skin", 0));
    }

    public void ChooseSkin()
    {
        skinChoice = Convert.ToInt32(gameObject.name);
        PlayerPrefs.SetInt("Skin", skinChoice);
    }
}
