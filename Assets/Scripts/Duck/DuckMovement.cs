using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DuckMovement : MonoBehaviour
{
    public float jumpForce = 5f;
    public float rotationSpeed = 5f;
    public bool isGrounded = true;
    public float startMomentum = 3f;
    public float momentum;
    public float momentumIncrease = 0.1f;
    public float graceTimer = 0.5f;
    public Rigidbody2D rb;
    public int backflipCount = 0;
    public bool isTargetHit = false;
    
    private void Start()
    {
        momentum = startMomentum;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //when space is pressed or screen touched, the duck jumps
        if (Input.anyKey || Input.touchCount > 0)
        {
            Jump();
        }
    }
    
    void Jump()
    {
        //if the duck is on the ground, it jumps
        if (isGrounded)
        {
            //duck jumps with forward momentum speed and rotation speed
            rb.velocity = new Vector2(momentum, jumpForce);
            rb.rotation = rotationSpeed;
            CameraShake.Instance.ShakeCamera(5f, 0.15f);

            //duck is no longer grounded
            isGrounded = false;
        }

        //if player tries to jump when the duck is not grounded
        if (!isGrounded)
        {
            //reset momentum
            momentum = startMomentum;
        }
    }
    
    void OnCollisionEnter2D(Collision2D collision)
    {
        if(isGrounded){return;}
        
        //if the duck collides with the ground, it is grounded
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            //start grace timer, if duck jumps within grace timer duration, duck gains momentum, else momentum is reset
            StartCoroutine(GraceTimer());
        }
        
        //if the duck collides with the target, the challenge is completed
        if (collision.gameObject.CompareTag("Target"))
        {
            isTargetHit = true;
            // print("Target hit");
        }
    }
    
    IEnumerator GraceTimer()
    {
        yield return new WaitForSeconds(graceTimer);
        if (!isGrounded)
        {
            momentum = startMomentum;
        }
        //increase momentum
        momentum += momentumIncrease;
    }
    

    //check if backflip is preformed
    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            //if duck is grounded and rotation is 360, backflip is preformed
            if (isGrounded && rb.rotation == 360)
            {
                //add to backflip count
                backflipCount++;
                //reset rotation
                rb.rotation = 0;
            }
        }
    }
}
