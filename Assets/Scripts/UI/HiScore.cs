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
        Hi_Score.text = PlayerPrefs.GetInt("HighScore", 0).ToString();
        Bread.text = PlayerPrefs.GetInt("Bread", 0).ToString();

    }


    void Update()
    {
        Hi_Score.text = PlayerPrefs.GetInt("HighScore", 0).ToString();
        Bread.text = PlayerPrefs.GetInt("Bread", 0).ToString();
    }
}
