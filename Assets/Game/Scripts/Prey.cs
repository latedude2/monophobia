using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prey : MonoBehaviour {

    public float loneliness = 0;
    public bool alive = true;
    private CrowdManager crowdManager;

    // Start is called before the first frame update
    void Start() {
        crowdManager = FindObjectOfType<CrowdManager>();
    }

    // Update is called once per frame
    void Update() {
        UpdateLoneliness();
    }

    public void UpdateLoneliness() {
        float minDist = 0;
        float outerMost = 0;
        foreach (Prey prey in crowdManager.GetAllPrey()) {
            float dist = Vector2.Distance((Vector2)prey.transform.position, (Vector2)transform.position);
            if (minDist == 0) {
                minDist = dist;
            } else if (dist < minDist && dist != 0) {
                minDist = dist;
            }
            outerMost += dist;
        }
        outerMost /= crowdManager.GetAllPrey().Count;
        loneliness = minDist * crowdManager.distFactor + outerMost * crowdManager.outerFactor;
    }
}
