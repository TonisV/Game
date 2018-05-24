using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PortalTeleport : MonoBehaviour {

    public int DestinationSceneIndex;
    public Light triggerLight;

    private int activeSceneIndex;
    private bool portalTriggered;

    private void Update() {
        if (portalTriggered) {
            if (triggerLight.range < 15) {
                triggerLight.range += 10 * Time.deltaTime;
            } else {
                SceneManager.LoadScene(DestinationSceneIndex);
                PlayerPrefs.SetInt("sceneIndex", DestinationSceneIndex);
                PlayerPrefs.SetInt("LevelUp", 1);
                PlayerPrefs.DeleteKey("health");
            }
        }
    }

    void OnTriggerEnter2D(Collider2D col) {
        if (col.CompareTag("Player")) {
            portalTriggered = true;
        }
    }
}
