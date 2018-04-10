using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikes : MonoBehaviour {
    private PlayerController playerController;
    private GameObject player;
    private Transform playerTransform;
    private Rigidbody2D playerRb;

    private bool startRotation = false;
    private bool rotationStop = false;
    private float zRotation = 0;

    private bool startMoving = false;
    private bool stopMoving = false;
    private float xPosition;

    private float distance;
    private float time;
    private float speed;

    public bool rotate;
    public bool shoot;


    void Start() {
        player = GameObject.FindGameObjectWithTag("Player");
        playerController = player.GetComponent<PlayerController>();
        playerTransform = player.transform;
        playerRb = player.GetComponent<Rigidbody2D>();
    }

    void OnTriggerEnter2D(Collider2D col) {
        if(col.CompareTag("Player")) {
            if (rotate) {
                startRotation = true;
            } else if (shoot) {
                startMoving = true;
                xPosition = transform.parent.position.x;
            } else {
                playerController.Die();
            }
        }
    }

    void Update() {
        if (startRotation && rotationStop == false) {
            if (zRotation < 90) {
                zRotation += 5;
                transform.parent.eulerAngles = new Vector3(0, 0, zRotation);
            } else if (zRotation == 90) {
                rotationStop = true;
            }
            transform.parent.position = new Vector2(-13.50f, -10);
        } else if (startMoving && stopMoving == false) {
            if (xPosition < -7.3) {

                // Calculate the speed needed to reach player
                distance = xPosition - playerTransform.position.x;
                time = playerTransform.position.y / playerRb.velocity.y;
                speed = distance / time;

                xPosition += -(speed);
                transform.parent.position = new Vector2(xPosition, transform.parent.position.y);
            } else {
                stopMoving = true;
            }
        }
    }

}
