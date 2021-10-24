using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BiasMovement : MonoBehaviour
{

    [SerializeField] private float y = 3f;
    [SerializeField] private float x = 2f;
    void FixedUpdate()
    {
        Vector2 moveDirection = new Vector2(x, y);
        if(transform.position.y > 0)
            moveDirection.y = -y;
        else moveDirection.y = y;

        GetComponent<Rigidbody2D>().AddForce(moveDirection);
    }
}
