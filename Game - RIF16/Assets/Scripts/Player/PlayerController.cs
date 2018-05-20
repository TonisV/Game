using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class PlayerController : PhisicObject {

    public float maxSpeed = 7;

    [Header("Jumping")]
    public float jumpTakeOffSpeed = 7;

    [Header("Health")]
    public int maxHealth = 5;
    public static int curHealth;
    private DateTime lastRun;
    public Transform spawningPoint;

    [Header("Effects")]
    public AudioSource runAudioSource;
    public AudioSource jumpAudioSource;
    public AudioSource hurtAudioSource;

    private SpriteRenderer spriteRenderer;
    private CapsuleCollider2D playerCollider;
    private Animator animator;
    private bool playerFlipX = false;


    // Use this for initialization
    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();

        curHealth = maxHealth;
    }

    protected override void ComputeVelocity()
    {
        Vector2 move = Vector2.zero;

        move.x = Input.GetAxisRaw("Horizontal");

        if (Input.GetButtonDown("Jump") && grounded)
        {
            velocity.y = jumpTakeOffSpeed;
        }
        else if (Input.GetButtonUp("Jump"))
        {
            if (velocity.y > 0)
            {
                velocity.y = velocity.y * 0.5f;
            }
        }

        if (move.x > 0.01f)
        {   
            if (playerFlipX)
            {
                spriteRenderer.transform.Rotate(0, 180, 0,Space.Self);
                playerFlipX = false;
            }
        }

        else if (move.x < -0.01f)
        {   
            if (!playerFlipX)
            {
                spriteRenderer.transform.Rotate(0, 180, 0,Space.Self);
                playerFlipX = true;
            }
        }

        animator.SetBool("grounded", grounded);
        animator.SetFloat("velocityX", Mathf.Abs(velocity.x) / maxSpeed);

        // Set Velocity Directly
        targetVelocity = move * maxSpeed;
        
    }

    void OnCollisionEnter2D(Collision2D other) {
        if (other.transform.tag == "MovingPlatform")
        {
            transform.parent = other.transform;
        }
    }

    void OnCollisionExit2D(Collision2D other) {
        if (other.transform.tag == "MovingPlatform")
        {
            transform.parent = null;
        }
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Enemy"))
        {
            Die();
        }
    }

    void OnTriggerExit2D(Collider2D other) {
        if (other.CompareTag("LevelBorder"))
        {
            Die();
        }
    }

    public void Die() {
        if (lastRun.AddSeconds(2) < DateTime.Now) {

            lastRun = DateTime.Now;

            curHealth--;
            int ActiveSceneIndex = SceneManager.GetActiveScene().buildIndex;

            if (ActiveSceneIndex > 1) {
                if (curHealth == 0) {
                    SceneManager.LoadScene(ActiveSceneIndex - 1);
                } else {
                    transform.position = spawningPoint.position;
                }
            } else {
                if (curHealth == 0) {
                    SceneManager.LoadScene(ActiveSceneIndex);
                } else {
                    transform.position = spawningPoint.position;
                }
            }
        }
    }
}