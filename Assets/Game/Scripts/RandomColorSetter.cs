using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomColorSetter : MonoBehaviour {
    public List<Color> colors;

    // Start is called before the first frame update
    void Start() {
        if (colors.Count > 0) {
            int rand = Random.Range(0, colors.Count);
            GetComponent<SpriteRenderer>().material.SetColor("_ShirtColor", colors[rand]);
        }
    }

    // Update is called once per frame
    void Update() {
        
    }
}
