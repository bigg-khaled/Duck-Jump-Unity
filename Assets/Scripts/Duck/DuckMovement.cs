using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DuckMovement : MonoBehaviour
{
    public float startJumpForce = 5f;
    public float jumpForce;
    public float maxJumpForce = 7;
    public float rotationSpeed = 5f;
    public bool isGrounded = true;
    public float startMomentum = 3f;
    public float momentum;
    public float momentumIncrease = 0.1f;
    public float graceTimer = 0.5f;
    public Rigidbody2D rb;
    public int frontflipCount = 0;
    public bool isTargetHit = false;
    
    private void Start()
    {
        momentum = startMomentum;
        jumpForce = startJumpForce;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //when space is pressed or screen touched, the duck jumps
        if (Input.anyKey || Input.touchCount > 0)
        {
            Jump();
        }
        
        //check if the duck did a whole 360 spin
        CheckFrontFlip();

    }
    
    void Jump()
    {
        //if the duck is on the ground, it jumps
        if (isGrounded)
        {
            //duck jumps with forward momentum speed and rotation speed
            rb.velocity = new Vector2(momentum, jumpForce);
            rb.rotation += rotationSpeed;
            CameraShake.Instance.ShakeCamera(5f, 0.15f);
            
            //duck is no longer grounded
            isGrounded = false;
        }
    }
    
    void OnCollisionEnter2D(Collision2D collision)
    {
        if(isGrounded){return;}
        
        //if the duck collides with the ground, it is grounded
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            
            //keep rotation
            rb.rotation = 0f;
            //start grace timer, if duck jumps within grace timer duration, duck gains momentum, else momentum is reset
            StartCoroutine(GraceTimer());
        }
        
        //if the duck collides with the target, the challenge is completed
        if (collision.gameObject.CompareTag("Target"))
        {
            isTargetHit = true;

            //this command deletes insures no other eggs are there once the challenge is finished
            collision.gameObject.tag = "Broken";
            GameObject[] targets = GameObject.FindGameObjectsWithTag("Target");
            foreach (GameObject target in targets)
            {
                Destroy(target);
            }
            // print("Target hit");
        }
    }
    
    IEnumerator GraceTimer()
    {
        yield return new WaitForSeconds(graceTimer);
        if (!isGrounded)
        {
            momentum = startMomentum;
            jumpForce = startJumpForce;
        }
        //increase momentum
        momentum *= 1f + momentumIncrease;

        if (jumpForce <= maxJumpForce)
        {
            jumpForce *= 1f + momentumIncrease;
        }
        
    }

    private void CheckFrontFlip()
    {
        if (rb.rotation <= -360f)
        {
            rb.rotation = 0f;
            frontflipCount++;
            //print("Frontflip count: " + frontflipCount);
        }
    }

}
    

