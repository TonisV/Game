using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour {

    private Image healthHearts;
    private float waitTime = 5.0f;
    private float targetFillAmount;

    public GameObject NextLevelUI;
    public GameObject StartHelpUI;


    // Use this for initialization
    void Start () {
        healthHearts = GameObject.Find("HealthHearts").GetComponent<Image>();

        if (PlayerPrefs.HasKey("Downgraded") && PlayerPrefs.GetInt("Downgraded") == 1) {
            NextLevelUI.GetComponent<Image>().color = new Color32(143,22,26,176);
            NextLevelUI.SetActive(value: true);
            Time.timeScale = 0;

            PlayerPrefs.SetInt("Downgraded", 0);
        } else if (PlayerPrefs.HasKey("LevelUp") && PlayerPrefs.GetInt("LevelUp") == 1) {
            NextLevelUI.GetComponent<Image>().color = new Color32(125, 240, 8, 176);
            NextLevelUI.SetActive(value: true);
            Time.timeScale = 0;

            PlayerPrefs.SetInt("LevelUp", 0);
        } else if (!PlayerPrefs.HasKey("health") || PlayerPrefs.GetInt("health") == 5) {
                StartHelpUI.SetActive(value: true);
                Time.timeScale = 0;
        }

        
    }

    // Update is called once per frame
    void Update () {
        targetFillAmount = PlayerController.curHealth * 0.2f;

        if (healthHearts.fillAmount > targetFillAmount) {
            if (healthHearts.fillAmount - targetFillAmount > 0.2f) {
                healthHearts.fillAmount -= healthHearts.fillAmount - targetFillAmount;
                healthHearts.fillAmount += 0.2f;
            }

            healthHearts.enabled = true;

            healthHearts.fillAmount -= 1.0f / waitTime * Time.deltaTime;
        } else {
            healthHearts.enabled = true;
        }

        if (StartHelpUI.activeInHierarchy) {
            if (Input.anyKeyDown) {
                StartHelpUI.SetActive(value: false);
                NextLevelUI.SetActive(value: true);
            }
        }
    }
}
