using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadSceneOnClick : MonoBehaviour {
    private int savedSceneIndex;
    public Button continueButton;
    public Text continueText;

    public bool dontUpdateSavedScene;

    void Start() {
        savedSceneIndex = PlayerPrefs.GetInt("sceneIndex");
        if(savedSceneIndex > 1 && continueButton != null && continueText != null) {
            continueButton.interactable = true;
            continueText.color = new Color(1f, 1f, 1f, 1f);
        }
    }

    public void LoadByIndex(int sceneIndex) {
        if (!dontUpdateSavedScene) {
            PlayerPrefs.SetInt("sceneIndex", sceneIndex);
        }
        Time.timeScale = 1;
        SceneManager.LoadScene(sceneIndex);
    }

    public void LoadFromSave() {
        if (savedSceneIndex > 1) {
            Time.timeScale = 1;
            SceneManager.LoadScene(savedSceneIndex);
        }
    }
}
