using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Stamina : MonoBehaviour
{
    public float stamina = 100f;
    [SerializeField] private float staminaRegenSpeed;

    void Start()
    {
        
    }

    void Update()
    {
        RegenerateStamina();
    }

    public void UseStamina(float staminaUsed)
    {
        stamina -= staminaUsed; 
    }

    void RegenerateStamina()
    {
        if(stamina < 100f)
        {
            stamina += staminaRegenSpeed * Time.deltaTime;
        }
    }
}
