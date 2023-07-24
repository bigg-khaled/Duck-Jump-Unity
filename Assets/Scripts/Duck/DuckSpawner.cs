using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DuckSpawner : MonoBehaviour
{
    public GameObject duck;


    void OnCollisionEnter2D(Collision2D collision)
    {
        // print("Egg hit the ground");
        //if the duck hits the target, the duck is destroyed and a new duck is spawned
        if (collision.gameObject.CompareTag("Player"))
        {
            //spawn the duck
            duck.GetComponent<SpriteRenderer>().enabled = true;
            duck.GetComponent<DuckMovement>().scriptEnabled = true;

            //give duck a little jump forward
            duck.GetComponent<Rigidbody2D>().velocity = new Vector2(1f, 1f);
            
            //destroy the egg
            Destroy(gameObject);
        }
        
    }
}