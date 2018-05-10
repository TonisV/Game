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
        if (other.transform.tag == "Enemy")
        {
           Die();
        }
        
        if (other.transform.tag == "MovingPlatform")
        {
            transform.parent = other.transform;
        }
        if (other.transform.tag == "Ice")
        {
           onIce = true; 
        }
    }

    void OnCollisionExit2D(Collision2D other) {
        if (other.transform.tag == "MovingPlatform")
        {
            transform.parent = null;
        }
        if (other.transform.tag == "Ice")
        {
           onIce = false; 
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