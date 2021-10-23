using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrowdManager : MonoBehaviour {
    public GameObject boidPrefab;
    public float distFactor = 1;
    public float outerFactor = 1;

    // Start is called before the first frame update
    void Start() {
    }

    void Update() {
        // foreach (Boid boid in GetAllBoids()) {
        //     boid.UpdateLoneliness();
        // }

        // Instantiate mock boids with mouse for testing purposes
        if (boidPrefab) {
            if (Input.GetMouseButtonDown(0)) {
                Vector2 position = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition);
                GameObject boid = Instantiate(boidPrefab, position, Quaternion.identity);
                boid.transform.SetParent(transform);
            }
        }
    }

    public List<Prey> GetAllBoids() {
        Transform[] children = GetComponentsInChildren<Transform>();
        List<Prey> Out = new List<Prey>();
        foreach (Transform child in children) {
            Prey boid;
            if (child.TryGetComponent<Prey>(out boid)) {
                Out.Add(child.GetComponent<Prey>());
            }
        }
        return Out;
    }

    public Prey GetMostLonely() {
        List<Prey> boids = GetAllBoids();
        if (boids.Count == 0) {
            return null;
        }
        var mostLonely = boids[0];
        foreach (Prey boid in boids) {
            if (boid.loneliness > mostLonely.loneliness) {
                mostLonely = boid;
            }
        }
        return mostLonely;
    }

    public bool TryGetMostLonely(out Prey mostLonely) {
        Prey boid = GetMostLonely();
        if (boid != null) {
            mostLonely = boid;
            return true;
        } else {
            mostLonely = null;
            return false;
        }
    }
}
