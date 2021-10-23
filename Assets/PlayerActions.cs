using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActions : MonoBehaviour
{
    [SerializeField] Transform targetPushPosition;
    [SerializeField] float pushMaxDistance;
    [SerializeField] float pushForce;
    [SerializeField] float pushCooldown;

    bool pushReady;

    void Start()
    {
        
    }

    void Update()
    {

        if (Input.GetMouseButtonDown(0) && pushReady)
        {
            PushEntity();
        }
        if (Input.GetMouseButtonDown(1))
        {
            GrabEntity();
        }
    }

    void PushEntity()
    {
        GameObject[] runners = GameObject.FindGameObjectsWithTag("Runner");

        GameObject closestRunnerHit = null;
        float closestRunnerDist = 100;
        foreach(GameObject runner in runners)
        {
            float dist = Vector3.Distance(runner.transform.position, targetPushPosition.position);
            if (dist <= pushMaxDistance)
            {
                if (dist < closestRunnerDist)
                {
                    closestRunnerHit = runner;
                }
            }
        }
        if (closestRunnerHit != null)
        {
            closestRunnerHit.GetComponent<Rigidbody2D>().AddForce(transform.up * pushForce);
            StartCoroutine(PushCooldown());
        }
    }

    IEnumerator PushCooldown()
    {
        pushReady = false;
        yield return new WaitForSeconds(pushCooldown);
        pushReady = true;
    }

    void GrabEntity()
    {

    }
}
