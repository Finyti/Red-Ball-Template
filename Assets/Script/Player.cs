using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Rigidbody2D rb;

    public float jumpSpeed = 11f;
    public float moveSpeed = 0.7f;
    public float maxSpeed = 7f;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }


    void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            rb.velocity += Vector2.up * jumpSpeed;

        }
        var hor = Input.GetAxis("Horizontal");

        if(rb.velocity.x < maxSpeed && rb.velocity.x > -maxSpeed)
        {
            print(rb.velocity.x);
            rb.AddForce(new Vector2(hor * moveSpeed, 0));
        }

        

    }
}
