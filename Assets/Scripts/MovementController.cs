using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class MovementController : MonoBehaviour
{
    float move;
    public int speed;

    Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        move = Input.GetAxis("Horizontal");

        Flip();
    }

    void FixedUpdate()
    {
        rb.velocity = new Vector2(move*speed, rb.velocity.y);
    }

    void Flip()
    {
        // When walking backwards flip
        if (rb.velocity.x < 0) transform.rotation = Quaternion.Euler(0, 180, 0);
        // When walking forwards unflip
        else if (rb.velocity.x > 0) transform.rotation = Quaternion.identity;
    }
}
