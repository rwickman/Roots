using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : Health
{
   public Color hurtColor;

    public float hurtColorTime = 0.1f;
    private float timeSinceHurt = 0.0f;
    private SpriteRenderer renderer;

    
    void Start()
    {
        curHealth = maxHealth;
        renderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        timeSinceHurt += Time.deltaTime;
        if (timeSinceHurt >= hurtColorTime)
        {
            renderer.color = Color.white;
        }
        
    }

    public override void Damage(float hitPoints)
    {
        curHealth -= hitPoints;
        renderer.color = hurtColor;
        if (curHealth <= 0.0f)
        {
            //Destroy(gameObject);
        } 
        timeSinceHurt = 0.0f;
    }
}
