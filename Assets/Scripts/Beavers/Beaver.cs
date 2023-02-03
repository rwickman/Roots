using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Beaver : MonoBehaviour
{
    public float speed = 1.0f;
    public float stoppingDistance = 1.0f;

    public float damage = 1.0f;

    private float lastDamageTime;
    private bool isAttacking = false;
    
    GameObject player;
    
    Animator animator;
 
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        MoveTowardsPlayer();
    }

    public void DamagePlayer()
    {
            // Damage the player
            PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
            playerHealth.Damage(damage);
    }

    void MoveTowardsPlayer()
    {
        // Move towards the player
        Vector3 dir =  (player.transform.position - transform.position).normalized;
        
        if (Vector3.Distance(player.transform.position, transform.position) > stoppingDistance)
        {
            transform.position = transform.position + dir * speed * Time.deltaTime;
            animator.SetBool("IsWalking", true);
            animator.SetBool("IsAttacking", false);
            isAttacking = false;
        }
        else
        {
            animator.SetBool("IsWalking", false);
            animator.SetBool("IsAttacking", true);
            if (!isAttacking)
            {
                lastDamageTime = 0.0f;
            }
            isAttacking = true;
        }
        
        // Set the animation direction
        animator.SetFloat("XInput", dir.x);
        animator.SetFloat("YInput", dir.y);        
    }

}
