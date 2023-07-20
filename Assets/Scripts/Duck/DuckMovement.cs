using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DuckMovement : MonoBehaviour
{
    public float speed = 1f;
    public float jumpForce = 5f;
    public bool isGrounded = true;
    public float groundCheckRadius = 0.2f;
    public LayerMask groundLayer;
    public Transform groundCheck;
    
    public float momentum = 0f;
    public float momentumMax = 10f;
    public float gracePeriod = 0.1f;
    public float graceTimer = 0f;
    public Rigidbody2D rb;

    //when screen is tapped duck jumps then falls
    //if pressed when duck touches ground within grace period
    //duck jumps again with forward momentum 

    // Start is called before the first frame update
    void Start()
    {
        
        
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
            rb.velocity = Vector3.up * jumpForce;
            isGrounded = false;
        }
    }
    
    void OnCollisionEnter2D(Collision2D collision)
    {
        //if the duck collides with the ground, it is grounded
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }
}
