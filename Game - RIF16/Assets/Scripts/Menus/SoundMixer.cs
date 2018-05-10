using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SoundMixer : MonoBehaviour {

	public AudioMixer masterMixer;

	public Slider masterVolumeSlider;
	public Slider musicVolumeSlider;
	public Slider sfxVolumeSlider;

	void Start() {
		float masterVol = PlayerPrefs.GetFloat("MasterVol", 0f);
		float musicVol = PlayerPrefs.GetFloat("MusicVol", 0f);
		float sfxVol = PlayerPrefs.GetFloat("SFXVol", 0f);

		masterVolumeSlider.value = masterVol;
		musicVolumeSlider.value = musicVol;
		sfxVolumeSlider.value = sfxVol;

		SetMasterLvl(masterVol);
		SetMasterLvl(musicVol);
		SetMasterLvl(sfxVol);
	}


	public void SetMasterLvl(float masterLvl) {
		masterMixer.SetFloat("masterVol", masterLvl);
		//Update PlayerPrefs
		PlayerPrefs.SetFloat("MasterVol", masterLvl);
		//Save changes
		PlayerPrefs.Save();
	}
	public void SetMusicLvl(float musicLvl) {
		masterMixer.SetFloat("musicVol", musicLvl);
		//Update PlayerPrefs
		PlayerPrefs.SetFloat("MusicVol", musicLvl);
		//Save changes
		PlayerPrefs.Save();
	}

	public void SetSfxLvl(float sfxLvl) {
		masterMixer.SetFloat("sfxVol", sfxLvl);
		//Update PlayerPrefs
		PlayerPrefs.SetFloat("SFXVol", sfxLvl);
		//Save changes
		PlayerPrefs.Save();
	}
}
