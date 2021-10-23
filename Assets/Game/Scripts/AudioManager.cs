using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class AudioManager : MonoBehaviour {

    public StudioEventEmitter soundtrack;
    [Range(0, 1)] public float intensity;

    void Start() {
        
    }

    void Update() {
        soundtrack.SetParameter("Intensity", intensity);
    }
}
