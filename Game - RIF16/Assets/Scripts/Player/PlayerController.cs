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
    public bool playerHurt;
    public Transform spawningPoint;

    [Header("Effects")]
    public AudioSource runAudioSource;
    public AudioSource jumpAudioSource;
    public AudioSource hurtAudioSource;

    private SpriteRenderer spriteRenderer;
    private CapsuleCollider2D playerCollider;
    private Animator animator;
    private bool jump;


    /* Initialization */
    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();

        if (PlayerPrefs.HasKey("health")) {
            curHealth = PlayerPrefs.GetInt("health");
            if (curHealth == 0) {
                curHealth = maxHealth;
            }
        } else {
            curHealth = maxHealth;
        }
    }


    /* Player Movement */
    protected override void ComputeVelocity()
    {
        Vector2 move = Vector2.zero;

        move.x = !playerHurt ? Input.GetAxisRaw("Horizontal") : 0;

        if (Input.GetButtonDown("Jump") && grounded && !playerHurt)
        {
            velocity.y = jumpTakeOffSpeed;
            jump = true;
        }
        else if (Input.GetButtonUp("Jump") && !playerHurt)
        {
            if (velocity.y > 0)
            {
                velocity.y = velocity.y * 0.5f;
            }
            jump = false;
        }

        if (move.x > 0.01f)
        {   
            spriteRenderer.flipX = false;
        }
        else if (move.x < -0.01f)
        {   
            spriteRenderer.flipX = true;
        }

        animator.SetBool("grounded", grounded);
        animator.SetBool("playerHurt", playerHurt);
        animator.SetFloat("velocityX", Mathf.Abs(velocity.x) / maxSpeed);

        // Set Velocity Directly
        targetVelocity = move * maxSpeed; 
    }


    /* Player Sounds */
    public void playJumpSound()
    {
        if (jump)
        {
            jumpAudioSource.Play();
        }
    }

    public void playRunSound()
    {
        runAudioSource.pitch = 1 + UnityEngine.Random.Range(-0.2f, 0.2f);
        runAudioSource.Play();
    }

    public void playHurtSound()
    {
        hurtAudioSource.Play();
    }


    /* Player Collisions and Triggers*/
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
            playerHurt = true;
        }
    }

    void OnTriggerExit2D(Collider2D other) {
        if (other.CompareTag("LevelBorder"))
        {
            playerHurt = true;
        }
    }

    public void Die() {
        
        if (lastRun.AddSeconds(0.5) < DateTime.Now) {

            lastRun = DateTime.Now;

            curHealth--;
            PlayerPrefs.SetInt("health", curHealth);

            int ActiveSceneIndex = SceneManager.GetActiveScene().buildIndex;

            if (ActiveSceneIndex > 1) {
                if (curHealth == 0) {
                    SceneManager.LoadSceneAsync(ActiveSceneIndex - 1);
                } else {
                    SceneManager.LoadSceneAsync(ActiveSceneIndex);
                }
            } else {
                SceneManager.LoadSceneAsync(ActiveSceneIndex);
            }
        }
    }

    void OnApplicationQuit() {
        PlayerPrefs.DeleteKey("health");
    }
}