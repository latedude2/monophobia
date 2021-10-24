using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float walkSpeed;
    [SerializeField] float sprintSpeed;
    [SerializeField] float sprintStaminaCost;

    private Rigidbody2D rigidBody;
    private Stamina stamina;

    private Animator animator;
    bool tired = false;

    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        stamina = GetComponent<Stamina>();
        animator = transform.Find("Visuals").GetComponent<Animator>();
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
        if (Input.GetKey(KeyCode.LeftShift) && !stamina.tired)
        {
            rigidBody.AddForce(direction * sprintSpeed * Time.deltaTime);
            stamina.UseStamina(sprintStaminaCost * Time.deltaTime);
            if(animator.GetCurrentAnimatorClipInfo(0).Length > 0 && animator.GetCurrentAnimatorClipInfo(0)[0].clip.name != "PushingWalking")
                animator.Play("Running");
        }
        else
        {
            if(direction.magnitude > 0)
            {
                if(animator.GetCurrentAnimatorClipInfo(0).Length > 0 && animator.GetCurrentAnimatorClipInfo(0)[0].clip.name != "PushingWalking")
                    animator.Play("Walking");
            }
            else{
                if(animator.GetCurrentAnimatorClipInfo(0).Length > 0 && animator.GetCurrentAnimatorClipInfo(0)[0].clip.name != "PushingWalking")
                    animator.Play("Idle");
            }
            rigidBody.AddForce(direction * walkSpeed * Time.deltaTime);
        }
    }
}
