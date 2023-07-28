using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HiScore : MonoBehaviour
{
    public TextMeshProUGUI Hi_Score;
    public TextMeshProUGUI Bread;
    
    void Start()
    {
        GameObject hiScoreObj = GameObject.Find("Hi_Score");
        if (hiScoreObj != null)  Hi_Score.text = PlayerPrefs.GetInt("HighScore", 0).ToString();

        Bread.text = PlayerPrefs.GetInt("Bread", 0).ToString();

    }


    void Update()
    {
        GameObject hiScoreObj = GameObject.Find("Hi_Score");
        if (hiScoreObj != null) Hi_Score.text = PlayerPrefs.GetInt("HighScore", 0).ToString();
        Bread.text = PlayerPrefs.GetInt("Bread", 0).ToString();
    }
}
