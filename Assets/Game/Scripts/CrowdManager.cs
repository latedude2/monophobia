using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrowdManager : MonoBehaviour {
    public GameObject preyPrefab;
    public uint initialCrowdSize = 100;
    public float initialCrowdRadius = 10;
    
    // Values for tuning loneliest calculation
    public float distFactor = 1;
    public float outerFactor = 1;

    [SerializeField] float musicIntensity = 0f;

    private GameObject player;

    // Start is called before the first frame update
    void Start() {
        player = GameObject.FindGameObjectWithTag("Player");
        SpawnCrowd(initialCrowdSize, (Vector2)player.transform.position, initialCrowdRadius);
    }

    void Update() {
        AdjustMusicIntensity();
        // Instantiate mock boids with mouse for testing purposes
        /*
        if (preyPrefab) {
            if (Input.GetMouseButtonDown(0)) {
                Vector2 position = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition);
                SpawnCrowd(10, position, 2);
            }
        }
        */
    }

    void AdjustMusicIntensity()
    {
        musicIntensity = 1f - ((float)GetComponentsInChildren<Transform>().Length / (float)initialCrowdSize);
        GameObject.Find("Music").GetComponent<AudioManager>().intensity = musicIntensity;
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
        if (player != null) {
            if (player.TryGetComponent<Prey>(out Prey playerPrey)) {
                Out.Add(playerPrey);
            }
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

    public void SpawnCrowd(uint crowdSize, Vector2 center, float radius) {
        for (int i = 0; i < crowdSize; i++) {
            Vector2 randDir = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f));
            randDir.Normalize();
            Vector2 pos = center + randDir * Mathf.Pow(Random.Range(0f, radius), 0.5f);
            GameObject prey = Instantiate(preyPrefab, new Vector3(pos.x, pos.y, 0), Quaternion.identity);
            prey.transform.SetParent(transform);
            
        }
    }
}
