using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeOutEffect : MonoBehaviour {
	
	public GameObject objectToFade;
	private SpriteRenderer spriteRenderer;
	private float fadeValue = 1f;
	private float currentTime = 0f;
	public float timeItTakesToFade = 0.5f;
	private bool isFading = false;
	public bool useCollider = false;
	public bool fadeOutAndDestroy = false;

	
	void Start() {
		if (objectToFade == null)
		{
			objectToFade = this.gameObject;
		}
		spriteRenderer = objectToFade.GetComponent<SpriteRenderer>();

	}
	

	void Update() {
		if(isFading){
			currentTime += Time.deltaTime;
			if(currentTime <= timeItTakesToFade){
				if (fadeValue <= 0.1f && fadeOutAndDestroy)
				{
					Destroy(objectToFade);
				}
				else
				{
					fadeValue = 1f - (currentTime / timeItTakesToFade);
					spriteRenderer.color = new Color(1f,1f,1f, fadeValue);
				}
			}
		}
	}

 
    void OnTriggerEnter2D(Collider2D other) {
		if (!useCollider)
		{
			if (other.CompareTag("Player"))
			{
				currentTime = 0;
				isFading = true;
			} 
		}
    }


	void OnCollisionEnter2D(Collision2D other) {
		if (useCollider)
		{
			if (other.transform.tag == "Player")
			{
				currentTime = 0;
				isFading = true;
			}
		}
    }
}
