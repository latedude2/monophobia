using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActions : MonoBehaviour
{
    [SerializeField] Transform targetInteractPosition;
    [SerializeField] float targetInteractDistance;
    [SerializeField] float interactMaxDistance;
    [SerializeField] float pushForce;
    [SerializeField] float pushCooldown;
    [SerializeField] float grabForce;

    bool pushReady = true;
    GameObject grabTarget = null;

    void Start()
    {
        targetInteractPosition.transform.localPosition = new Vector3(0, targetInteractDistance/2, 0);
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
        HoldEntity();
        if (Input.GetMouseButtonUp(1))
        {
            LetGoEntity();
        }
    }

    GameObject LocateActionableEntity()
    {
        List<GameObject> targets = new List<GameObject>();

        foreach (GameObject runner in GameObject.FindGameObjectsWithTag("Runner"))
        {
            float dist = Vector3.Distance(runner.transform.position, targetInteractPosition.position);
            if (dist <= targetInteractDistance)
            {
                targets.Add(runner);
            }
        }
        foreach (GameObject actionableObject in GameObject.FindGameObjectsWithTag("ActionableObject"))
        {
            float dist = Vector3.Distance(actionableObject.transform.position, targetInteractPosition.position);
            if (dist <= targetInteractDistance)
            {
                targets.Add(actionableObject);
            }
        }

        GameObject closestTargetHit = null;
        float closestTargetDist = 100;
        foreach (GameObject target in targets)
        {
            float dist = Vector3.Distance(target.transform.position, transform.position);
            if (dist <= interactMaxDistance)
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
        grabTarget = LocateActionableEntity();
    }

    void HoldEntity()
    {
        if (grabTarget != null)
        {
            grabTarget.GetComponent<Rigidbody2D>().AddForce((targetInteractPosition.position - grabTarget.transform.position) * grabForce * Time.deltaTime);
        
            float dist = Vector3.Distance(grabTarget.transform.position, transform.position);
            if (dist > interactMaxDistance)
            {
                grabTarget = null;
            }
        }
        

    }

    void LetGoEntity()
    {
        if (grabTarget != null)
        {
            grabTarget = null;
        }
    }
}
