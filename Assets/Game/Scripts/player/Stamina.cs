using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Stamina : MonoBehaviour
{
    private float stamina = 100f;
    [SerializeField] private float staminaRegenSpeed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
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
