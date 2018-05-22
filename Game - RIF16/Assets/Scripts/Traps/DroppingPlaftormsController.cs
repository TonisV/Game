using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroppingPlaftormsController : MonoBehaviour {

	private Rigidbody2D body2d;
	public bool usedrop = false;
	public bool instantDrop;
	public Transform rayStart;
	public Transform rayEnd;
	private bool playerSpotted = false;
	public int randomNumber;
	public int dropSpeedModifier = 1;
	public int gravityModifier = 1;

	void Start () {
		randomNumber = Random.Range(20, (30 * dropSpeedModifier));
		body2d = GetComponent<Rigidbody2D>();
	}
	
	void Update () {
		Raycasting();
		platformDropping();
	}

	// Drop platform when player is randomly spotted
	public void platformDropping() {
		if (usedrop)
		{
			if (instantDrop && playerSpotted)
			{
				randomNumber = 0;
			}
			if (playerSpotted)
			{
				randomNumber--;
			}
			if (randomNumber < 1)
			{
				body2d.bodyType = RigidbodyType2D.Dynamic;
				body2d.gravityScale = gravityModifier;
			}
		}
	}

	// Cast ray in choosen position and check if it is colliding with player
	public void Raycasting() {
		Debug.DrawLine(rayStart.position, rayEnd.position, Color.red);
		playerSpotted = Physics2D.Linecast(rayStart.position, rayEnd.position, 1 << LayerMask.NameToLayer("Player"));
	}
	
}
