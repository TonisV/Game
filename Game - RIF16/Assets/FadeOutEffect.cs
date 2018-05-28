﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeOutEffect : MonoBehaviour {

	private float time = 10.0f;
	
	public GameObject objectToFade;
	private SpriteRenderer spriteRenderer;
	private float fadeValue = 1f;
	private float currentTime = 0f;
	public float timeItTakesToFade = 0.5f;
	private bool isFading = false;

	
	void Start() {
		spriteRenderer = objectToFade.GetComponent<SpriteRenderer>();
	}
	
	void Update() {
		if(isFading){
			currentTime += Time.deltaTime;
				if(currentTime <= timeItTakesToFade){
				fadeValue = 1f - (currentTime / timeItTakesToFade);
				spriteRenderer.color = new Color(1f,1f,1f, fadeValue);
				}
		}
	}
 
     void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player"))
        {
        	currentTime = 0;
     		isFading = true;
        }
    }
}
