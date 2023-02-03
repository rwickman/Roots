using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{

    public float speed = 5.0f;
    public float damage = 1.0f;

    [HideInInspector]
    public Vector3 dir;


    public string enemyTag = "Player";
    private string environmentTag = "Environment";
    public float liveTime = 5.0f;

    private float timeSinceSpawn = 0.0f;
    // Update is called once per frame
    void Update()
    {
        //transform.position = transform.position + dir * speed * Time.deltaTime;
        timeSinceSpawn += Time.deltaTime;
        if (timeSinceSpawn >= liveTime)
        {
            Destroy(gameObject);
        }
    }

    public void Fire(Vector3 lookDir)
    {
        GetComponent<Rigidbody2D>().velocity = new Vector2(lookDir.x, lookDir.y).normalized * speed;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.tag == enemyTag)
        {   
            Health health = col.GetComponent<Health>();
            health.Damage(damage);
            Destroy(gameObject);
        }
        else if(col.gameObject.tag == environmentTag)
        {
            Destroy(gameObject);
        }
    }
}
