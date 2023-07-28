using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DuckSpawner : MonoBehaviour
{
    public GameObject[] duck;
    //public ParticleSystem eggCrack;

    private void Awake()
    {
        GameObject objectToSpawn = duck[PlayerPrefs.GetInt("Skin", 1)];
        Instantiate(objectToSpawn, transform.position, Quaternion.identity);
        //eggCrack = GetComponent<ParticleSystem>();
        //eggCrack.Stop();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {

        //play the egg crack particle effect
        //eggCrack.Play();

        //destroy the egg
        //Destroy(gameObject);
    }
}