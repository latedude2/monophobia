using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerControls : MonoBehaviour
{
    [SerializeField] int speed;
    private Rigidbody2D rigidBody;

    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        var direction = transform.up * Input.GetAxis("Vertical") + transform.right * Input.GetAxis("Horizontal");
        if (direction.magnitude > 1.0f)
        {
            direction.Normalize();
        }
        rigidBody.AddForce(direction * speed * Time.deltaTime);
    }
}
