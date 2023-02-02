using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{

    public float speed = 5.0f;
    [HideInInspector]
    
    public Vector3 dir;
    public float liveTime = 5.0f;

    private float timeSinceSpawn = 0.0f;
    // Update is called once per frame
    void Update()
    {
        transform.position = transform.position + dir * speed * Time.deltaTime;
        timeSinceSpawn += Time.deltaTime;
        if (timeSinceSpawn >= liveTime)
        {
            Destroy(gameObject);
        }
    }
}
