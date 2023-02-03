using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Track the state the boss is in based on the current health
public class StageTracker : MonoBehaviour
{
    
    public float[] stateHealthPct = new float[3]{1.0f, 0.75f, 0.25f};
    private EnemyHealth health;
    
    // Start is called before the first frame update
    void Start()
    {
        health = GetComponent<EnemyHealth>();
    }

 
    // Update is called once per frame
    void Update()
    {
        
    }

    public int GetStage()
    {
        for (int i = stateHealthPct.Length - 1; i >= 0; i--)
        {
            if (health.GetHealth()/health.GetMaxHealth() <= stateHealthPct[i]){
               return i;
            }
        }
        return 0;
    }

}
