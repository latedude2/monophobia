using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrowdManager : MonoBehaviour {
    public GameObject preyPrefab;
    public float distFactor = 1;
    public float outerFactor = 1;
    private GameObject player;

    // Start is called before the first frame update
    void Start() {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update() {
        // Instantiate mock boids with mouse for testing purposes
        if (preyPrefab) {
            if (Input.GetMouseButtonDown(0)) {
                Vector2 position = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition);
                GameObject prey = Instantiate(preyPrefab, position, Quaternion.identity);
                prey.transform.SetParent(transform);
            }
        }
    }

    public List<Prey> GetAllPrey() {
        Transform[] children = GetComponentsInChildren<Transform>();
        List<Prey> Out = new List<Prey>();
        foreach (Transform child in children) {
            Prey prey;
            if (child.TryGetComponent<Prey>(out prey)) {
                Out.Add(child.GetComponent<Prey>());
            }
        }
        if (player.TryGetComponent<Prey>(out Prey playerPrey)) {
            Out.Add(playerPrey);
        }
        return Out;
    }

    public Prey GetMostLonely() {
        List<Prey> allPrey = GetAllPrey();
        if (allPrey.Count == 0) {
            return null;
        }
        var mostLonely = allPrey[0];
        foreach (Prey prey in allPrey) {
            if (prey.loneliness > mostLonely.loneliness) {
                mostLonely = prey;
            }
        }
        return mostLonely;
    }

    public bool TryGetMostLonely(out Prey mostLonely) {
        Prey prey = GetMostLonely();
        if (prey != null) {
            mostLonely = prey;
            return true;
        } else {
            mostLonely = null;
            return false;
        }
    }
}
