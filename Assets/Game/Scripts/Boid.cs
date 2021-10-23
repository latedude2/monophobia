using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boid : MonoBehaviour {

    public float loneliness = 0;
    public bool alive = true;
    private CrowdManager boidManager;

    // Start is called before the first frame update
    void Start() {
        boidManager = FindObjectOfType<CrowdManager>();
    }

    // Update is called once per frame
    void Update() {
        
    }

    public void UpdateLoneliness() {
        loneliness = 0;
        foreach (Boid boid in boidManager.GetAllBoids()) {
            loneliness += Vector2.Distance((Vector2)boid.transform.position, (Vector2)transform.position);
        }
    }
}
