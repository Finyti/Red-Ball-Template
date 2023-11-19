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

        var hor = Input.GetAxis("Horizontal");

        if(rb.velocity.x < maxSpeed && rb.velocity.x > -maxSpeed)
        {
            rb.drag = 1;
            rb.AddForce(new Vector2(hor * moveSpeed, 0));
        }
        else
        {
            rb.drag = 10;
        }
        //print(rb.velocity.magnitude);

        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 0.6f);
        //Debug.DrawRay(transform.position, new Vector2(0, -0.6f), Color.green);
        if (hit)
        {
            if (Input.GetButtonDown("Jump"))
            {
                rb.velocity += Vector2.up * jumpSpeed;

            }
        }
        

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name.Contains("Flag"))
        {
            GameManager.instance.Win();
        }
        if (collision.gameObject.name.Contains("Spike"))
        {
            GameManager.instance.Loose();
        }
        if (collision.gameObject.name.Contains("Coin"))
        {
            GameManager.instance.CoinAdd(collision.gameObject);
        }
    }
}
