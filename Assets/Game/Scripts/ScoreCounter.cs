using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreCounter : MonoBehaviour
{
    float timeSurvived = 0;
    public float milliSeconds;
    public float seconds;
    public float minutes;
    bool alive = true;

    Text textField;

    void Start()
    {
        textField = GetComponent<Text>();
    }

    void Update()
    {
        if (alive)
        {
            timeSurvived += Time.deltaTime;
            minutes = Mathf.FloorToInt(timeSurvived / 60);
            seconds = Mathf.FloorToInt(timeSurvived % 60);
            milliSeconds = Mathf.FloorToInt((timeSurvived * 60) % 60);
        }
    }

    public void Show()
    {
        alive = false;

        textField.text = "You survived for " + minutes + "m " + seconds + "." + milliSeconds + "s";
    }
}
