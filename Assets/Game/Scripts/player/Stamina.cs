using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Stamina : MonoBehaviour
{
    public float stamina = 100f;
    public bool tired = false;
    [SerializeField] private float staminaRegenSpeed;

    void Start()
    {
        
    }

    void Update()
    {
        if(stamina < 0)
        {
            tired = true;
            Invoke(nameof(Recover), 1f);
        }

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
    void Recover()
    {
        tired = false;
    }
}
