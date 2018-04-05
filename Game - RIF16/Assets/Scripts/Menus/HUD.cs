using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour {

    private Text HealthText;

    // Use this for initialization
    void Start () {
        HealthText = GameObject.Find("HealthText").GetComponent<Text>();

    }
	
	// Update is called once per frame
	void Update () {
        HealthText.text = "Health " + PlayerController.curHealth + "/5";
    }
}
