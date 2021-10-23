using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prey : MonoBehaviour {

    public float loneliness = 0;
    public bool alive = true;
    private CrowdManager boidManager;

    // Start is called before the first frame update
    void Start() {
        boidManager = FindObjectOfType<CrowdManager>();
    }

    // Update is called once per frame
    void Update() {
        UpdateLoneliness();
    }

    public void UpdateLoneliness() {
        float minDist = 0;
        float outerMost = 0;
        foreach (Prey boid in boidManager.GetAllBoids()) {
            float dist = Vector2.Distance((Vector2)boid.transform.position, (Vector2)transform.position);
            if (minDist == 0) {
                minDist = dist;
            } else if (dist < minDist && dist != 0) {
                minDist = dist;
            }
            outerMost += dist;
        }
        outerMost /= boidManager.GetAllBoids().Count;
        loneliness = minDist * boidManager.distFactor + outerMost * boidManager.outerFactor;
    }
}
