using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TryAgain : MonoBehaviour
{
    //onclick, reload the scene
    public void TryAgainLevel()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainGame");
    }
}
