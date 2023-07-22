using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeaGull : MonoBehaviour
{
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        

        if (gameObject.CompareTag("Broken"))
        {
            // Get the Rigidbody2D component of the GameObject
            Rigidbody2D rb = GetComponent<Rigidbody2D>();

            // Set the gravity scale to 1
            rb.gravityScale = 1f;
            rb.velocity = new Vector2(0f, rb.velocity.y);
        }
        else
        {
            transform.position += Vector3.left * speed * Time.deltaTime;
        }
    }
}
