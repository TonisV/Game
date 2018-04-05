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
	public bool playerSpotted = false;

	// Use this for initialization
	void Start () {
		currentPoint = points[pointSelection];
	}
	
	// Update is called once per frame
	void Update () {
		Raycasting();
		if (playerSpotted)
		{
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

	// Cast ray in choosen position and check if it is collaiding with player
	public void Raycasting() {
		Debug.DrawLine(rayStart.position + currentPoint.position, rayEnd.position + currentPoint.position, Color.red);
		playerSpotted = Physics2D.Linecast(rayStart.position, rayEnd.position, 1 << LayerMask.NameToLayer("Player"));
	}
}
