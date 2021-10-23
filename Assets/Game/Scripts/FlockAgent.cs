using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class FlockAgent : MonoBehaviour
{

    Collider2D agentCollider;
    public Collider2D AgentCollider { get { return agentCollider; } }
    
    private Rigidbody2D rigidBody;



    // Start is called before the first frame update
    void Start()
    {
        agentCollider = GetComponent<Collider2D>();
        rigidBody = GetComponent<Rigidbody2D>();
    }

    public void Move(Vector2 velocity)
    {
        transform.right = velocity;
        rigidBody.AddForce(velocity * Time.deltaTime * 2, ForceMode2D.Impulse);
    }
}
