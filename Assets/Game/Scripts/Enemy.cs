using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    public GameObject aimer;
    public GameObject shadowMonster;
    private GameObject _aimer;
    private GameObject _shadowMonster;
    public int monsterSpeed;
    CrowdManager crowdManager;
    public CharacterGenerator charGen;
    private GameObject currentTarget;
    [SerializeField] [Min(0.001f)] private float attackTime = 1;

    void Start() {
        crowdManager = FindObjectOfType<CrowdManager>();
        InvokeRepeating(nameof(AttackLoneliest), attackTime, attackTime);
        _aimer = Instantiate(aimer);
    }

    private void Update() {
        Hunt();
        if (_shadowMonster != null) 
        { 
            MonsterBehavior();
        }
    }

    void Hunt() {
        if (crowdManager.TryGetMostLonely(out Prey target)) {
            _aimer.transform.position = target.transform.position;
        }
    }

    void AttackLoneliest() {
        if (crowdManager.TryGetMostLonely(out Prey target)) {
            currentTarget = target.gameObject;
            _shadowMonster = Instantiate(shadowMonster, new Vector3(currentTarget.transform.position.x - 5, currentTarget.transform.position.y, 0), Quaternion.identity);
        }
    }

    void MonsterBehavior()
    {
        Vector3 dir = currentTarget.transform.position - _shadowMonster.transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        _shadowMonster.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        _shadowMonster.transform.Translate(Vector3.right * monsterSpeed * Time.deltaTime);

        float dist = Vector3.Distance(_shadowMonster.transform.position, currentTarget.transform.position);
        if (dist < .01f)
        {
            Kill();
            Destroy(_shadowMonster);
        }
    }

    void Kill()
    {
        Destroy(currentTarget.gameObject);
        charGen.showDeadCharacter();
    }
}
