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
        GameObject uiObj = GameObject.Find("UI");
        if (uiObj != null)
        {
            Hi_Score = uiObj.transform.Find("Hi_Score").GetComponent<TextMeshProUGUI>();
            Bread = uiObj.transform.Find("Bread").GetComponent<TextMeshProUGUI>();
        }

        if (Hi_Score != null)
        {
            Hi_Score.text = PlayerPrefs.GetInt("HighScore", 0).ToString();
        }

        if (Bread != null)
        {
            Bread.text = PlayerPrefs.GetInt("Bread", 0).ToString();
        }
    }

    void Update()
    {
        if (Hi_Score != null)
        {
            Hi_Score.text = PlayerPrefs.GetInt("HighScore", 0).ToString();
        }

        if (Bread != null)
        {
            Bread.text = PlayerPrefs.GetInt("Bread", 0).ToString();
        }
    }
}
