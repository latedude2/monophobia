using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BiasMovement : MonoBehaviour
{
    private void Start() {
        GetComponent<Animator>().Play("Walking");
    }

    [SerializeField] private float y = 3f;
    [SerializeField] private float x = 2f;
    void FixedUpdate()
    {
        Vector2 moveDirection = new Vector2(x, y);
        if(transform.position.y > 2.5)
            moveDirection.y = -y;
        else if(transform.position.y < -2.5)
            moveDirection.y = y;
        else moveDirection.y = 0;

        GetComponent<Rigidbody2D>().AddForce(moveDirection);
    }
}
