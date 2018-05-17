using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class PlayerController : PhisicObject {

    public float maxSpeed = 7;
    public float jumpTakeOffSpeed = 7;
    public static int curHealth;
    public int maxHealth = 5;
    private DateTime lastRun;

    public Transform spawningPoint;

    private SpriteRenderer spriteRenderer;
    private Animator animator;

    public bool onIce = false;
    public bool enemyHit = false;


    // Use this for initialization
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
            if (spriteRenderer.flipX == true)
            {
                spriteRenderer.flipX = false;
            }
        }
        else if (move.x < -0.01f)
        {
            if (spriteRenderer.flipX == false)
            {
                spriteRenderer.flipX = true;
            }
        }

        animator.SetBool("grounded", grounded);
        animator.SetFloat("velocityX", Mathf.Abs(velocity.x) / maxSpeed);

        if(onIce){
            // Apply input as a force instead of setting velocity directly.
            targetVelocity = move * maxSpeed;
        }else{
            // Set Velocity Directly
            targetVelocity = move * maxSpeed;
        }
    }

    void OnCollisionEnter2D(Collision2D other) {
        if (other.transform.tag == "MovingPlatform")
        {
            transform.parent = other.transform;
        }
        if (other.transform.tag == "Ice")
        {
           //onIce = true; 
        }
    }

    void OnCollisionExit2D(Collision2D other) {
        if (other.transform.tag == "MovingPlatform")
        {
            transform.parent = null;
        }
        if (other.transform.tag == "Ice")
        {
           //onIce = false; 
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