using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    public GameObject aimer;
    private GameObject _aimer;
    BoidManager boidManager;
    [SerializeField] [Min(0.001f)] private float attackTime = 1;

    void Start() {
        boidManager = FindObjectOfType<BoidManager>();
        InvokeRepeating("AttackLoneliest", attackTime, attackTime);
        _aimer = Instantiate(aimer);
    }

    private void Update() {
        Hunt();
    }

    void Hunt() {
        if (boidManager.TryGetMostLonely(out Boid target)) {
            _aimer.transform.position = target.transform.position;
        }
    }

    void AttackLoneliest() {
        if (boidManager.TryGetMostLonely(out Boid target)) {
            Destroy(target.gameObject);
        }
    }
}
