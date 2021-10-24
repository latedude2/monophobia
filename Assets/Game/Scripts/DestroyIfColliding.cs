using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyIfColliding : MonoBehaviour
{
    bool dontDestroy = false;
    private void Start() {
        Invoke(nameof(StopDestorying), 0.5f);
    }

    void StopDestorying()
    {
        dontDestroy = true;
    }

    void OnCollisionEnter(Collision collision)
    {
        if(dontDestroy == false)
        {
            if(collision.collider.GetComponent<DestroyIfColliding>() != null)
            {
                Debug.Log("Bugged car destroyed");
                Destroy(gameObject);
            }
        }
    }
}
