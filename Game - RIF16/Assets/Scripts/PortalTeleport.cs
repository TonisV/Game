using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PortalTeleport : MonoBehaviour {

    private PlayerController playerController;
    private GameObject player;
    private int activeSceneIndex;
    private string test;

    // Use this for initialization
    void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
        playerController = player.GetComponent<PlayerController>();
        activeSceneIndex = SceneManager.GetActiveScene().buildIndex;
        test = "INVALID";
    }

    void OnTriggerEnter2D(Collider2D col) {
        if (col.CompareTag("Player")) {
            if (SceneManager.GetSceneByBuildIndex(activeSceneIndex + 1).IsValid() ) {
                SceneManager.LoadScene(activeSceneIndex + 1);
            } else {
                SceneManager.LoadScene(0);
            }
        }
    }
}
