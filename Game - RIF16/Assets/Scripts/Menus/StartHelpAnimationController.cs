using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartHelpAnimationController : MonoBehaviour {
    public bool animationCompleted = false;

    public void StopAnimation() {
        this.GetComponent<Animator>().enabled = false;
        animationCompleted = true;
    }
}
