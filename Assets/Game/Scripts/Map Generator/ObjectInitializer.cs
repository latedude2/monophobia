using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectInitializer : MonoBehaviour
{
    public GameObject player;
    public GameObject respawnsPrefab;

    public GameObject[] respawns;

    GameObject[] ObjectsWithRespawn;

    GameObject[] GameObjectsFindRespawn()
    {
        respawns = GameObject.FindGameObjectsWithTag("Respawn");

        /*
        for (int i = 0; i < respawns.Length; i++)
        {
            Debug.Log("Here is respwans " + respawns[i]);
        }
        */
        return respawns;
        
    }

    Vector2 PlayerPosition()
    {
        Vector3 currentplayerposition;
        Vector2 v2Currentplayerposition;


        currentplayerposition = player.transform.position;

        v2Currentplayerposition = currentplayerposition;

        return v2Currentplayerposition;

    }

    void DistanceAndInitialize(Vector2[] gameObjectsPositions, Vector2 playerPosition)
    {
        float distance;
        
        for (int i = 0; i < gameObjectsPositions.Length; i++)
        {
            distance = Vector2.Distance(gameObjectsPositions[i], playerPosition);
            
            if(distance > )
        }




    }

    void DespawnGameObject()
    {

    }

    Vector2[] GameObjectsPositions(GameObject[] ObjectsWithRespawn)
    {

        Vector3[] V3GameObjectsPositions = new Vector3[ObjectsWithRespawn.Length]; 
        /*
        for (int i = 0; i < ObjectsWithRespawn.Length; i++)
        {
            Debug.Log("Here is array from GameObjectsPositions1 " + ObjectsWithRespawn[i]);
        }  
        */
        
        for(int i = 0; i < ObjectsWithRespawn.Length; i++)
        {
            V3GameObjectsPositions[i] = (ObjectsWithRespawn[i]).transform.position;

            //Debug.Log("Object positions " + V3GameObjectsPositions[i]);
        }
        
        Vector2[] V2GameObjectsPositions = new Vector2[V3GameObjectsPositions.Length];
        //V2GameObjectsPositions = (Vector2) V3GameObjectsPositions;
        for(int i = 0; i < ObjectsWithRespawn.Length; i++)
        {

            V2GameObjectsPositions[i] = V3GameObjectsPositions[i];

            //Debug.Log("Object positions " + V2GameObjectsPositions[i]);

        }
 
        return V2GameObjectsPositions;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        ObjectsWithRespawn = GameObjectsFindRespawn();
        PlayerPosition();
        DistanceAndInitialize(GameObjectsPositions(ObjectsWithRespawn), PlayerPosition());
        
    }
}
