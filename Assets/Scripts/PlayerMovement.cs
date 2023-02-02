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
    
    private float timeSinceLastFire = 1000.0f;
    private Vector3 moveInput;
    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
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
        GameObject shotProjectile = Instantiate(projectile, transform.position, Quaternion.identity);
        if (curDirection == PlayerDirection.Right)
        {
            shotProjectile.GetComponent<Projectile>().dir = Vector3.right;
            shotProjectile.transform.Rotate(0, 0, 90);
        }
        else if(curDirection == PlayerDirection.Left)
        {
            shotProjectile.GetComponent<Projectile>().dir = Vector3.left;
            shotProjectile.transform.Rotate(0, 0, -90);
        }
        else if (curDirection == PlayerDirection.Up)
        {
            shotProjectile.GetComponent<Projectile>().dir = Vector3.up;
        }
        else
        {
            shotProjectile.GetComponent<Projectile>().dir = Vector3.down;
            shotProjectile.transform.Rotate(0, 0, 180);
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
