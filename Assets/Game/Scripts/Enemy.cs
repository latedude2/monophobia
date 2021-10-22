using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    BoidManager boidManager;
    [SerializeField] [Min(0.001f)] private float attackTime = 1;

    void Start() {
        boidManager = FindObjectOfType<BoidManager>();
        InvokeRepeating("AttackLoneliest", attackTime, attackTime);
    }

    void AttackLoneliest() {
        boidManager.DestroyMostLonely();
    }
}
