using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] int walkSpeed;
    [SerializeField] int sprintSpeed;
    [SerializeField] int sprintStaminaCost;

    private Rigidbody2D rigidBody;
    private Stamina stamina;

    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        stamina = GetComponent<Stamina>();
    }

    void Update()
    {
        Move();
    }

    void Move()
    {
        var direction = transform.up * Input.GetAxis("Vertical") + transform.right * Input.GetAxis("Horizontal");
        if (direction.magnitude > 1.0f)
        {
            direction.Normalize();
        }
        if (Input.GetKey(KeyCode.LeftShift) && stamina.stamina > 0)
        {
            rigidBody.AddForce(direction * sprintSpeed * Time.deltaTime);
            stamina.UseStamina(sprintStaminaCost * Time.deltaTime);
        }
        else
        {
            rigidBody.AddForce(direction * walkSpeed * Time.deltaTime);
        }
    }
}
