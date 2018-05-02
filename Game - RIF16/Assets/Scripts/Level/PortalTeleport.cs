using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PortalTeleport : MonoBehaviour {

    public int DestinationSceneIndex;

    private PlayerController playerController;
    private GameObject player;
    private int activeSceneIndex;

    // Use this for initialization
    void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
        playerController = player.GetComponent<PlayerController>();
    }

    void OnTriggerEnter2D(Collider2D col) {
        if (col.CompareTag("Player")) {
            SceneManager.LoadScene(DestinationSceneIndex);
            PlayerPrefs.SetInt("sceneIndex", DestinationSceneIndex);
        }
    }
}
