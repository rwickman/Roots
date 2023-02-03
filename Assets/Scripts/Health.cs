using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public float maxHealth = 10;

    protected float curHealth;
    
    // Start is called before the first frame update
    void  Start()
    {
        curHealth = maxHealth;
    }

    public virtual void Damage(float hitPoints)
    {
        curHealth -= hitPoints; 
    }

    public float GetHealth()
    {
        return curHealth;
    }

    public float GetMaxHealth()
    {
        return maxHealth;
    }
}
