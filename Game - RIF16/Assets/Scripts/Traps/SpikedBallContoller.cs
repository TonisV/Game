using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikedBallContoller : MonoBehaviour {

    private Rigidbody2D body2d;
    public float leftPushRange = -0.3f;
    public float rightPushRange = 0.3f;
    public float velocityThreshold = 90f;
    public bool useChainBreak = false;
    public int cravityModifier = 2;
    public Transform rayEnd;
    private bool playerSpotted = false;
    public int randomNumber;


    void Start() {
        randomNumber = Random.Range(10, 20);
        body2d = GetComponent<Rigidbody2D>();
        body2d.angularVelocity = velocityThreshold;
    }


    void Update() {
        Push();
        Raycasting();
        BreakChain();
    }


    // Push Rigidbody to make pendulum effect
    public void Push() {
        if (transform.rotation.z > 0 && transform.rotation.z < rightPushRange && (body2d.angularVelocity > 0) && body2d.angularVelocity < velocityThreshold) {
            body2d.angularVelocity = velocityThreshold;
        } else if (transform.rotation.z < 0 && transform.rotation.z > leftPushRange && (body2d.angularVelocity < 0) && body2d.angularVelocity > velocityThreshold * -1) {
            body2d.angularVelocity = velocityThreshold * -1;
        }
    }


    // Break the chain when player is randomly spotted and raise rigidbody cravity to make better collision effect
    public void BreakChain() {
        if (useChainBreak) {
            if (GetComponent<HingeJoint2D>()) {
                if (playerSpotted) {
                    randomNumber--;
                }
                if (randomNumber < 1) {
                    GetComponent<HingeJoint2D>().breakForce = 1f;
                    GetComponent<FixedJoint2D>().breakForce = 1f;
                    body2d.gravityScale = cravityModifier;
                }
            }
        }
    }


    // Cast ray in choosen position and check if it is collaiding with player
    public void Raycasting() {
        Debug.DrawLine(body2d.position, rayEnd.position, Color.red);
        playerSpotted = Physics2D.Linecast(body2d.position, rayEnd.position, 1 << LayerMask.NameToLayer("Player"));
    }
}