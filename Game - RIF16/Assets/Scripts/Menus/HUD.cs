using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour {

    private Image healthHearts;
    private float waitTime = 5.0f;
    private float targetFillAmount;

    public GameObject NextLevelUI;


    // Use this for initialization
    void Start () {
        healthHearts = GameObject.Find("HealthHearts").GetComponent<Image>();

        if (!PlayerPrefs.HasKey("health") || PlayerPrefs.GetInt("health") == 5) {
            NextLevelUI.SetActive(value: true);
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
    }
}
