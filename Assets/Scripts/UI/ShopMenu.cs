using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopMenu : MonoBehaviour
{
    public GameObject SkinMenu;
    public GameObject InAppMenu;
    
    public void ShowSkins()
    {
        SkinMenu.SetActive(true);
        InAppMenu.SetActive(false);
    }
    
    public void ShowInApp()
    {
        SkinMenu.SetActive(false);
        InAppMenu.SetActive(true);
    }
}
