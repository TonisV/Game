using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fade : MonoBehaviour {

    public Light lightToFade;
    public float eachFadeTime = 2f;
    public float fadeWaitTime = 5f;

    // Use this for initialization
    void Start () {
        StartCoroutine(FadeInAndOutRepeat(lightToFade, eachFadeTime, fadeWaitTime));
    }

    IEnumerator FadeInAndOut(Light lightToFade, bool fadeIn, float duration) {
        float minLuminosity = 2.5f; // min intensity
        float maxLuminosity = 7; // max intensity

        float counter = 0f;

        //Set Values depending on if fadeIn or fadeOut
        float a, b;

        if (fadeIn) {
            a = minLuminosity;
            b = maxLuminosity;
        } else {
            a = maxLuminosity;
            b = minLuminosity;
        }

        float currentIntensity = lightToFade.intensity;

        while (counter < duration) {
            counter += Time.deltaTime;

            lightToFade.intensity = Mathf.Lerp(a, b, counter / duration);

            yield return null;
        }
    }

    IEnumerator FadeInAndOutRepeat(Light lightToFade, float duration, float waitTime) {
        WaitForSeconds waitForXSec = new WaitForSeconds(waitTime);

        while (true) {
            //Fade out
            yield return FadeInAndOut(lightToFade, false, duration);

            //Wait
            yield return waitForXSec;

            //Fade-in 
            yield return FadeInAndOut(lightToFade, true, duration);
        }
    }
}
