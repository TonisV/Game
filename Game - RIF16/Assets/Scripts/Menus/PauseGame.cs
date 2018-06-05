using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;


public class PauseGame : MonoBehaviour {
    public Transform canvas;
    public GameObject NextLevelUI;
    public GameObject StartHelpUI;
    public GameObject StartComic;

    public AudioMixerSnapshot paused;
    public AudioMixerSnapshot unpaused;
    

    // Update is called once per frame
    void Update () {
        if (canvas == null) {
            return;
        }

        if (Input.GetKeyDown(KeyCode.Escape)) {
            if (NextLevelUI == null || StartHelpUI == null || StartComic == null) {
                Pause();
            } else {
                if (!NextLevelUI.activeInHierarchy && !StartHelpUI.activeInHierarchy && !StartComic.activeInHierarchy) {
                    Pause();
                }
            }
        }
    }

    public void Pause() {
        GameObject FirstChild = canvas.gameObject.transform.GetChild(0).gameObject;
        GameObject SecondChild = canvas.gameObject.transform.GetChild(1).gameObject;
        GameObject ThirdChild = canvas.gameObject.transform.GetChild(2).gameObject;

        if (canvas.gameObject.activeInHierarchy == false) {
            canvas.gameObject.SetActive(true);
            Time.timeScale = 0;
        } else {
            if (SecondChild.activeInHierarchy == true) {
                SecondChild.SetActive(false);
                FirstChild.SetActive(true);
            } else if (ThirdChild.activeInHierarchy == true) {
                ThirdChild.SetActive(false);
                FirstChild.SetActive(true);
            } else {
                canvas.gameObject.SetActive(false);
                Time.timeScale = 1;
            }
        }

        PauseAudio();
    }

    public void Restart() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1;
    }

    public void Unpause() {
        NextLevelUI.SetActive(value: false);
        Time.timeScale = 1;
    }

    public void PauseAudio() {
        if (Time.timeScale == 0)
        {
            paused.TransitionTo(.01f);
        } else {
            unpaused.TransitionTo(.01f);
        }
    }
}
