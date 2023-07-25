using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DuckSpawner : MonoBehaviour
{
    public GameObject duck;
    public ParticleSystem eggCrack;

    //private void Awake()
    //{
    //    eggCrack = GetComponent<ParticleSystem>();
    //    eggCrack.Stop();
    //}

    //void OnCollisionEnter2D(Collision2D collision)
    //{
    //    //spawn the duck
    //    duck.GetComponent<SpriteRenderer>().enabled = true;
    //    duck.GetComponent<DuckMovement>().scriptEnabled = true;
        
    //    //spin the egg
    //    GetComponent<Rigidbody2D>().angularVelocity = 10f;

    //    //give duck a little jump forward
    //    duck.GetComponent<Rigidbody2D>().velocity = new Vector2(1f, 2f);
        
    //    //play the egg crack particle effect
    //    eggCrack.Play();

    //    //destroy the egg
    //    Destroy(gameObject);
    //}
}