using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoidManager : MonoBehaviour {
    
    // Start is called before the first frame update
    void Start() {
    }

    void Update() {
        foreach (Boid boid in GetAllBoids()) {
            boid.UpdateLoneliness();
        }
        if (TryGetMostLonely(out Boid lonelyBoid)) {
            lonelyBoid.GetComponent<SpriteRenderer>().color = Color.red;
        }
    }

    public List<Boid> GetAllBoids() {
        Transform[] children = GetComponentsInChildren<Transform>();
        List<Boid> Out = new List<Boid>();
        foreach (Transform child in children) {
            Boid boid;
            if (child.TryGetComponent<Boid>(out boid)) {
                Out.Add(child.GetComponent<Boid>());
            }
        }
        return Out;
    }

    public Boid GetMostLonely() {
        List<Boid> boids = GetAllBoids();
        if (boids.Count == 0) {
            return null;
        }
        var mostLonely = boids[0];
        foreach (Boid boid in boids) {
            if (boid.loneliness > mostLonely.loneliness) {
                mostLonely = boid;
            }
        }
        return mostLonely;
    }

    public bool TryGetMostLonely(out Boid mostLonely) {
        Boid boid = GetMostLonely();
        if (boid != null) {
            mostLonely = boid;
            return true;
        } else {
            mostLonely = null;
            return false;
        }
    }

    public void DestroyMostLonely() {
        if (TryGetMostLonely(out Boid boid)) {
            Destroy(boid.gameObject);
        }
    }
}
