using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkinSwitch : MonoBehaviour
{
    public int skinChoice;
    public bool isUnlocked = false;
    public GameObject Lock;
    public GameObject Button;
    public TextMeshProUGUI Price;
    public Image BG;
    public Color offWhite = new Color(0.9f, 0.9f, 0.9f, 1f);


    private void Awake()
    {
        skinChoice = PlayerPrefs.GetInt("Skin", 0);
        isUnlocked = Convert.ToBoolean(PlayerPrefs.GetInt($"{gameObject.name}", 0));
    }
    void Start()
    {
        skinChoice = PlayerPrefs.GetInt("Skin", 0);
        BG = GetComponent<Image>();
        if(isUnlocked)
        {
            UnlockSkin();
        }
        if (PlayerPrefs.GetInt("Skin", 0) == Convert.ToInt32(gameObject.name))
        {
            BG.color = offWhite;
        }
    }


    void Update()
    {
        print(PlayerPrefs.GetInt("Skin", 0));
        if(PlayerPrefs.GetInt("Skin", 0) != Convert.ToInt32(gameObject.name))
        {
            BG.color = Color.black;
        }
    }

    public void ChooseSkin()
    {
        skinChoice = Convert.ToInt32(gameObject.name);
        PlayerPrefs.SetInt("Skin", skinChoice);
        BG.color = offWhite;
    }

    public void UnlockSkin()
    {
        if((PlayerPrefs.GetInt("Bread",0) < Convert.ToInt32(Price.text)) && !isUnlocked)
        {
            print("NO MONEY");
        }
        else
        {
            if (!isUnlocked)
            {
                PlayerPrefs.SetInt($"{gameObject.name}", 1);
                PlayerPrefs.SetInt("Bread", (PlayerPrefs.GetInt("Bread", 0) - Convert.ToInt32(Price.text)));
            }
            isUnlocked = true;

            Destroy(Lock);
            Destroy(Button);
        }
        

    }
}
