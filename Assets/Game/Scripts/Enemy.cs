using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    public GameObject aimer;
    private GameObject _aimer;
    CrowdManager crowdManager;
    [SerializeField] [Min(0.001f)] private float attackTime = 1;

    void Start() {
        crowdManager = FindObjectOfType<CrowdManager>();
        InvokeRepeating(nameof(AttackLoneliest), attackTime, attackTime);
        _aimer = Instantiate(aimer);
    }

    private void Update() {
        Hunt();
    }

    void Hunt() {
        if (crowdManager.TryGetMostLonely(out Prey target)) {
            _aimer.transform.position = target.transform.position;
        }
    }

    void AttackLoneliest() {
        if (crowdManager.TryGetMostLonely(out Prey target)) {
            Destroy(target.gameObject);

        }
    }
}
