using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BioInfoDisplay : MonoBehaviour {

    public float duration = 2;
    public float fadeTime = .001f;
    private CanvasGroup UIElements;
    void Start() {
        UIElements = GetComponent<CanvasGroup>();
    }

    public void Display() {
        CancelInvoke();
        UIElements.alpha = 1;
        InvokeRepeating(nameof(Fade), duration, fadeTime);
    }

    void Fade() {
        UIElements.alpha -= .01f;
    }
}
