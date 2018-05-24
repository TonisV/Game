using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxContoller : MonoBehaviour {

	public Transform[] backgrounds;
	private float[] parallaxMovements;
	public float smoothing = 1f;

	private Transform mainCamera;
	private Vector3 previousCameraPosition;


	void Awake () {
		mainCamera = Camera.main.transform;
	}

	void Start () {
		previousCameraPosition = mainCamera.position;

		parallaxMovements = new float[backgrounds.Length];
		for (int i = 0; i < backgrounds.Length; i++)
		{
			parallaxMovements[i] = backgrounds[i].position.z * -1;
		}
	}

	void Update () {
		for (int i = 0; i < backgrounds.Length; i++)
		{
			// Set oppsite direction of the camera
			float parallax = (previousCameraPosition.x - mainCamera.position.x) * parallaxMovements[i];
			// Set target x position for background
			float backgroundTargetPosX = backgrounds[i].position.x + parallax;
			// Create a target background position
			Vector3 backgroundTargetPos = new Vector3 (backgroundTargetPosX, backgrounds[i].position.y, backgrounds[i].position.z);

			// Smoothng
			backgrounds[i].position = Vector3.Lerp(backgrounds[i].position, backgroundTargetPos, smoothing * Time.deltaTime);
		}
		
		// Set previous camera position in each frame
		previousCameraPosition = mainCamera.position;
	}
}
