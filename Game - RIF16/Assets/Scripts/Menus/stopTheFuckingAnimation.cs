using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stopTheFuckingAnimation : MonoBehaviour {
    public void FuckingStop() {
        this.GetComponent<Animator>().enabled = false;
    }
}
