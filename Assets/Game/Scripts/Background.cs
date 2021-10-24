using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour
{
    List<GameObject> obstacles;
    [SerializeField] List<GameObject> obstaclePrefabs;

    [SerializeField] int obstacleCount = 3;

    private void Start() {
        obstacles = new List<GameObject>();
        //SpawnObstacles();
    }
    
    public void Move(float distance)
    {       
        transform.position = new Vector3(transform.position.x + distance, transform.position.y, transform.position.z); 
        foreach(GameObject obstacle in obstacles)
        {
            Debug.Log("Destroying");
            Destroy(obstacle);
        }
        SpawnObstacles();
    }

    void SpawnObstacles()
    {
        for(int i = 0; i < obstacleCount; i++)
        {
            GameObject obstacle = Instantiate(obstaclePrefabs[Random.Range(0, obstaclePrefabs.Count - 1)], 
                                              new Vector3(transform.position.x + Random.Range(-1.5f, 1.5f), transform.position.y + Random.Range(-3f, 3f), transform.position.z), 
                                              Quaternion.identity, gameObject.transform);
            obstacle.transform.Rotate(0.0f, 0.0f, Random.Range(0.0f, 360.0f));
            obstacles.Add(obstacle);
        }
    }
}
