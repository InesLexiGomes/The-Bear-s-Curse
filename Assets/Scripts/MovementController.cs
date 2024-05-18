using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class MovementController : MonoBehaviour
{
    Vector2 move;
    public int speed;

    Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        move = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        Flip();
    }

    void FixedUpdate()
    {
        rb.velocity = new Vector2(move.x*speed, rb.velocity.y);
    }

    void Flip()
    {
        if(move.x < -0.01f) transform.localScale = new Vector3(-1,1,1);
        if(move.x > 0.01f) transform.localScale = new Vector3(1,1,1);
    }
}
