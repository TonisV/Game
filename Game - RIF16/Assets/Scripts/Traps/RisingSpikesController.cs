using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RisingSpikesController : MonoBehaviour {
	
	public GameObject risingSpikes;
	public float risingHeight;
	public float risingSpeed;
	private bool playerSpotted;
	private Vector3 targetPosition;

	void Start() {
		targetPosition = new Vector3(risingSpikes.transform.position.x, risingSpikes.transform.position.y + risingHeight, risingSpikes.transform.position.z);
	}

	void Update()
	{
		if (playerSpotted)
		{
			RiseSpikes();
		}
	}

	void OnTriggerEnter2D(Collider2D other)
    {	
		if (other.CompareTag("Player"))
        {
            playerSpotted = true;
        }
    }

	public void RiseSpikes() {
		risingSpikes.transform.position = Vector3.MoveTowards(risingSpikes.transform.position, targetPosition, Time.deltaTime * risingSpeed);
	}
}
