using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] int speed;
    private Rigidbody2D rigidBody;

    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Walk();
    }

    void Walk()
    {
        var direction = transform.up * Input.GetAxis("Vertical") + transform.right * Input.GetAxis("Horizontal");
        if (direction.magnitude > 1.0f)
        {
            direction.Normalize();
        }
        rigidBody.AddForce(direction * speed * Time.deltaTime);
    }
}
