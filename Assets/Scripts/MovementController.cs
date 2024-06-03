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
        if(move < -0.01f) transform.localScale = new Vector3(-1,1,1);
        if(move > 0.01f) transform.localScale = new Vector3(1,1,1);
    }
}
