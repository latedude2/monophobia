using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActions : MonoBehaviour
{
    [SerializeField] Transform targetPushPosition;
    [SerializeField] float pushMaxDistance;
    [SerializeField] float pushForce;
    [SerializeField] float pushCooldown;

    bool pushReady = true;

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

    GameObject LocateActionableEntity()
    {
        List<GameObject> targets = new List<GameObject>();

        foreach (GameObject runner in GameObject.FindGameObjectsWithTag("Runner"))
        {
            targets.Add(runner);
        }
        foreach (GameObject actionableObject in GameObject.FindGameObjectsWithTag("ActionableObject"))
        {
            targets.Add(actionableObject);
        }

        GameObject closestTargetHit = null;
        float closestTargetDist = 100;
        foreach (GameObject target in targets)
        {
            float dist = Vector3.Distance(target.transform.position, targetPushPosition.position);
            if (dist <= pushMaxDistance)
            {
                if (dist < closestTargetDist)
                {
                    closestTargetHit = target;
                }
            }
        }
        if (closestTargetHit != null)
        {
            return closestTargetHit;
        }
        return null;
    }

    void PushEntity()
    {
        GameObject targetEntity = LocateActionableEntity();

        if (targetEntity != null)
        {
            targetEntity.GetComponent<Rigidbody2D>().AddForce(transform.up * pushForce);
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
        Debug.Log("hell");
    }
}
