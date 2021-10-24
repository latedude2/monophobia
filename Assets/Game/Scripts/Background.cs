using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour
{

    public void Move(float distance)
    {
        transform.position = new Vector3(transform.position.x + distance, transform.position.y, transform.position.z); 
    }
}
