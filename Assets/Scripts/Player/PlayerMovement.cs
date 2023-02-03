using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public enum PlayerDirection {
        Up,
        Right,
        Down,
        Left
    }

    public PlayerDirection curDirection;
    public float speed = 3.0f;
    public GameObject projectile;
    public float fireRate = 0.5f;

    public float lookDirOffset = 90.0f;
    
    private float timeSinceLastFire = 1000.0f;
    private Vector3 moveInput;
    private Rigidbody2D rb;
    private Vector3 lookDir;
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        moveInput = new Vector3(horizontal, vertical, 0.0f);
        UpdateCamera();
        UpdateDirection();
        timeSinceLastFire += Time.deltaTime;
        if (Input.GetButton("Fire1") && timeSinceLastFire >= fireRate)
        {
            Shoot();
            timeSinceLastFire = 0.0f;
        }
        UpdateAnimation();
        
    }

    void UpdateDirection()
    {
        if (moveInput.x > 0)
        {
            curDirection = PlayerDirection.Right;
        }
        else if(moveInput.x < 0)
        {
            curDirection = PlayerDirection.Left;
        }
        else if (moveInput.y > 0)
        {
            curDirection = PlayerDirection.Up;
        }
        else if (moveInput.y < 0)
        {
            curDirection = PlayerDirection.Down;
        }
        
    }

    // Shoot out a projectile towards an enemy
    void Shoot(){
        GameObject shotProjectileGO = Instantiate(projectile, transform.position, Quaternion.identity);
        Projectile shotProjectile = shotProjectileGO.GetComponent<Projectile>();
        shotProjectile.enemyTag = "Enemy";
        lookDir = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float lookAngle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;
        
        shotProjectile.dir = lookDir.normalized;
        shotProjectile.Fire(lookDir);
        
        shotProjectile.transform.rotation = Quaternion.Euler(0, 0, lookAngle + lookDirOffset);
        
    }

    void UpdateAnimation()
    {
        animator.SetBool("IsWalking", moveInput.x != 0.0f || moveInput.y != 0.0f);
        if (moveInput.x != 0.0f || moveInput.y != 0.0f)
        {
            animator.SetFloat("XInput", moveInput.x);
            animator.SetFloat("YInput", moveInput.y);
        }
        

    }

    void UpdateCamera(){
        Vector3 cameraPosition = new Vector3(transform.position.x, transform.position.y, Camera.main.transform.position.z);
        Camera.main.transform.position = cameraPosition;
    }

    // Update is called once per frame
    void FixedUpdate()
    {        
        Move();
    }

    void Move()
    {
        // Update player position
        Vector3 updatedPostiion = transform.position + moveInput * speed;
    
        rb.velocity = moveInput * speed;
        
    }
}
