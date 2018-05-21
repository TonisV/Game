using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySoundOnHit : MonoBehaviour {

	public AudioSource hitAudioSource;
	private bool soundPlayed = false;

	void OnTriggerEnter2D(Collider2D other)
    {	
		if (!soundPlayed)
		{
			if (other.CompareTag("Player") || other.CompareTag("Floor") || other.CompareTag("Enemy") )
			{
				hitAudioSource.Play();
				soundPlayed = true;	
			}
		}
    }
}
