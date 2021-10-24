using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundSpawner : MonoBehaviour
{
    [SerializeField] GameObject player;

    [SerializeField] List<GameObject> backgroundPatches;
    [SerializeField] GameObject backgroundPrefab;

    float distanceAhead = 35f;
    
    // Update is called once per frame
    void Update()
    {
        foreach(GameObject backgroundPatch in backgroundPatches)
        {
            if(backgroundPatch.transform.position.x + 15f < player.transform.position.x)
            {
                backgroundPatch.GetComponent<Background>().Move(distanceAhead);
            }
        }
    }
}
