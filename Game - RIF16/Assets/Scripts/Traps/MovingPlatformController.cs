using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatformController : MonoBehaviour {

	public GameObject platform;
	public float moveSpeed;
	private Transform currentPoint;
	public Transform[] points;
	public int pointSelection;
	public Transform rayStart;
	public Transform rayEnd;
	private bool playerSpotted = false;
	public bool moveWithPlayer = false;

	// Use this for initialization
	void Start () {
		currentPoint = points[pointSelection];
	}
	
	// Update is called once per frame
	void Update () {
		Raycasting();
		if (playerSpotted && moveWithPlayer)
		{
			MovePlatform();
		} else if(!moveWithPlayer) {
			MovePlatform();
		}
	}

	// Cast ray in choosen position and check if it is collaiding with player
	public void Raycasting() {
		Debug.DrawLine(rayStart.position, rayEnd.position, Color.red);
		playerSpotted = Physics2D.Linecast(rayStart.position, rayEnd.position, 1 << LayerMask.NameToLayer("Player"));
	}

	public void MovePlatform() {

		platform.transform.position = Vector3.MoveTowards(platform.transform.position, currentPoint.position, Time.deltaTime * moveSpeed);
	
		if (platform.transform.position == currentPoint.position)
		{
			pointSelection++;
			if (pointSelection == points.Length)
			{
				pointSelection = 0;
			}

			currentPoint = points[pointSelection];
		}	
	}
}
