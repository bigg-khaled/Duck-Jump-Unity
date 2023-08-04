using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiveBread : MonoBehaviour
{
    public int breadAmount;
    
    public void GivePlayerBread()
    {
        //give player bread amount
        int bread = PlayerPrefs.GetInt("Bread", 0);
        PlayerPrefs.SetInt("Bread", bread + breadAmount);   
        
    }
}
