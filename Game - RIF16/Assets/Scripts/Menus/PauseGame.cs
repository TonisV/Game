using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseGame : MonoBehaviour {
    public Transform canvas;

    // Update is called once per frame
    void Update () {
		if (Input.GetKeyDown(KeyCode.Escape)) {
            Pause();
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
    }

    public void Restart() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1;
    }

    public void Unpause() {
        Time.timeScale = 1;
    }
}
