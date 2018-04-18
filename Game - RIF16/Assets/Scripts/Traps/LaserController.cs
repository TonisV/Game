using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserController : MonoBehaviour {
	private PlayerController playerController;
	public Transform rayStart;
	public Transform rayEnd;
	LineRenderer laserLine;
	ParticleSystem laserParticles;
	private bool laserActive = false;
	private float laserTimer;
	public int laserTimerModifier = 3;
	private bool playerSpotted = false;

	void Start () {
		playerController = GameObject.Find("Player").GetComponent<PlayerController>();
		laserLine = GetComponent<LineRenderer>();
		laserLine.widthCurve = AnimationCurve.Linear(0, .5f, 1, .5f);
		laserParticles = gameObject.GetComponentInChildren<ParticleSystem>();
		laserTimer = Random.Range(1, laserTimerModifier);
	}
	
	void Update () {
		LaserTimer();
		PlayerHit();
	}

	public void LaserTimer () {

		laserLine.SetPosition(0, rayStart.position);
		laserLine.SetPosition(1, rayEnd.position);
		laserTimer -= Time.deltaTime;

		if (laserTimer <= 0 && laserActive)
		{
			laserTimer = Random.Range(1, laserTimerModifier);
			laserActive = false;
		} else if (laserTimer <= 0 && !laserActive) {
			laserTimer = Random.Range(1, laserTimerModifier);
			laserActive = true;
		}

		LaserToggle();
	}

	public void LaserToggle() {
		if (laserActive)
		{
			laserLine.enabled=true;
			laserParticles.Play(true);
		} else {
			laserLine.enabled=false;
			laserParticles.Stop(true);
		}
	}
	
	public void PlayerHit() {

		Raycasting();

		if (laserActive && playerSpotted)
		{
			playerController.Die();
		}
	}

	// Cast ray in choosen position and check if it is collaiding with player
	public void Raycasting() {
		Debug.DrawLine(rayStart.position, rayEnd.position, Color.red);
		playerSpotted = Physics2D.Linecast(rayStart.position, rayEnd.position, 1 << LayerMask.NameToLayer("Player"));
	}

}
