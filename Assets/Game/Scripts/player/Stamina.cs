using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using FMODUnity;

public class Stamina : MonoBehaviour
{
    public float stamina = 100f;
    public bool tired = false;
    public StudioEventEmitter heartbeat;
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

        heartbeat.SetParameter("Stamina", stamina);

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
