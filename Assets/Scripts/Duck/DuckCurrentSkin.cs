using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DuckCurrentSkin : MonoBehaviour
{
    public Sprite[] skinArray;
    public int currentSkinIndex;
    public Sprite currentSkin;

    private void Awake()
    {
        //get the skin from player pref
        currentSkinIndex = PlayerPrefs.GetInt("Skin", 1);
        
        //set the skin
        currentSkin = skinArray[currentSkinIndex];
        GetComponent<SpriteRenderer>().sprite = currentSkin;
    }
}
