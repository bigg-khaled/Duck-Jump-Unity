using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DuckMovement : MonoBehaviour
{
    public float startJumpForce = 5f;
    public float jumpForce;
    public float maxJumpForce = 7;
    public float jumpForceIncrease = 0.1f;
    public float rotationSpeed = 5f;
    public bool isGrounded = true;
    public float startMomentum = 3f;
    public float maxMomentum = 7f;
    public float momentum;
    public float momentumIncrease = 0.1f;
    public float graceTimer = 0.5f;
    public Rigidbody2D rb;
    public int frontflipCount = 0;
    public bool isTargetHit = false;
    public ChallengeHandler challengeHandler;
    private AudioSource audioSource;
    public AudioClip[] jumpSFX;
    public GameObject Particles;
    public int perfectJumpCount = 0;
    private float initialZRotation;
    public Canvas tapToStart;
    private float cameraShakeIntensity = 5f;
    private float cameraShakeDuration = 0.15f;

    private void Start()
    {
        initialZRotation = Particles.transform.rotation.eulerAngles.z;

        momentum = startMomentum;
        jumpForce = startJumpForce;


        //get sfx audio source
        audioSource = GameObject.Find("SFX").GetComponent<AudioSource>();
    }

    void Update()
    {
        Vector3 newRotation = Particles.transform.rotation.eulerAngles;
        newRotation.z = initialZRotation;
        Particles.transform.rotation = Quaternion.Euler(newRotation);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //place duck back if it falls off the screen
        if (transform.position.y < -10f)
        {
            challengeHandler.currentChallenge.FailChallenge();
        }

        //when any key is pressed, or screen touched, the duck jumps
        //make sure screen touch is below the pause button on the screen
        if (Input.GetKeyDown(KeyCode.Space) ||
            Input.touchCount > 0 && Screen.height - Input.GetTouch(0).position.y > 200f)
        {
            Time.timeScale = 1f;
            Jump();
        }

        //check if the duck did a whole 360 spin
        CheckFrontFlip();
    }

    private void Jump()
    {
        if (tapToStart.isActiveAndEnabled)
            tapToStart.enabled = false;

        //if the duck is on the ground, it jumps
        if (!isGrounded) return;

        //if the player jumps start the challenge
        if (!challengeHandler.isActiveAndEnabled && isGrounded)
        {
            challengeHandler.enabled = true;
            challengeHandler.gameObject.SetActive(true);
        }

        if (isGrounded)
        {
            //duck jumps with forward momentum speed and rotation speed
            rb.velocity = new Vector2(momentum, jumpForce);
            rb.rotation += rotationSpeed;
            CameraShake.Instance.ShakeCamera(cameraShakeIntensity, cameraShakeDuration);

            //play jump sound
            int randomJumpSound = UnityEngine.Random.Range(0, jumpSFX.Length);
            audioSource.PlayOneShot(jumpSFX[randomJumpSound]);

            //duck is no longer grounded
            isGrounded = false;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        //if the duck collides with the ground, it is grounded
        if (collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("Target") ||
            collision.gameObject.CompareTag("Broken"))
        {
            StopAllCoroutines();
            isGrounded = true;


            if (momentum <= maxMomentum)
            {
                momentum *= 1f + momentumIncrease;
            }


            if (jumpForce <= maxJumpForce)
            {
                jumpForce *= 1f + jumpForceIncrease;
            }

            //start streak
            JumpStreak();

            print(perfectJumpCount);

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

    void OnCollisionStay2D(Collision2D collision)
    {
        //insures egg is broken
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
        if (isGrounded)
        {
            momentum = startMomentum;
            jumpForce = startJumpForce;

            //reset streak
            ResetJumpStreak();
        }
    }

    void JumpStreak()
    {
        //particle effect with perfect jump
        Vector3 newRotation = Particles.transform.rotation.eulerAngles;
        newRotation.z = rb.rotation;
        Particles.transform.rotation = Quaternion.Euler(newRotation);
        Particles.GetComponent<ParticleSystem>().Play();
        perfectJumpCount++;

        //particle effect more than 3 perfect jumps
        if (perfectJumpCount is >= 3 and < 30)
        {
            //more intense particle effect 
            cameraShakeIntensity *= 1f + ((float)perfectJumpCount / 1000f);
            cameraShakeDuration *= 1f + ((float)perfectJumpCount / 1000f);

            //darker color current color with slight decrease in intensity
            Color currentColor = Particles.GetComponent<ParticleSystem>().startColor;
            currentColor.r += 0.01f;
            currentColor.g -= 0.01f;
            currentColor.b -= 0.01f;
            currentColor.a += 0.01f;
            Particles.GetComponent<ParticleSystem>().startColor = currentColor;

            //increase particle effect size
            ParticleSystem.MainModule main = Particles.GetComponent<ParticleSystem>().main;
            main.startSize = 0.1f * (1f + (perfectJumpCount / 10f));
            main.startSpeed = 0.1f * (1f + (perfectJumpCount / 10f));
            main.startLifetime = 0.1f * (1f + (perfectJumpCount / 10f));
        }
    }

    void ResetJumpStreak()
    {
        //disable particle effect
        Particles.GetComponent<ParticleSystem>().Stop();
        perfectJumpCount = 0;

        //reset particle effect
        ParticleSystem.MainModule main = Particles.GetComponent<ParticleSystem>().main;
        main.startSize = 0.1f;
        main.startSpeed = 5f;
        main.startLifetime = 1f;

        //reset color
        Particles.GetComponent<ParticleSystem>().startColor = Color.white;
    }

    private void CheckFrontFlip()
    {
        if (rb.rotation <= -360f)
        {
            rb.rotation = 0f;
            frontflipCount++;
        }
    }
}