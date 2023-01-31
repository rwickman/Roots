using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 0.1f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Move(horizontal, vertical);
    }

    void Move(float horizontal, float vertical)
    {
        horizontal = horizontal * speed;
        vertical = vertical * speed;
        Vector3 updatedPostiion = new Vector3(transform.position.x + horizontal, transform.position.y + vertical, 0.0f);

        transform.position = updatedPostiion;
    }
}
