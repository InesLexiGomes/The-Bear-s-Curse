using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class MovementController : MonoBehaviour
{
    [SerializeField] private int speed;
    [SerializeField] private int jumpSpeed;

    private float move;

    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;

    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        move = Input.GetAxis("Horizontal");

        if (Input.GetKeyDown("up") && IsGrounded())
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpSpeed);
        }

        // When walking backwards flip
        if (rb.velocity.x < 0) transform.rotation = Quaternion.Euler(0, 180, 0);
        // When walking forwards unflip
        else if (rb.velocity.x > 0) transform.rotation = Quaternion.identity;
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(move*speed, rb.velocity.y);
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCapsule(groundCheck.position, new Vector2(5f, 20f), CapsuleDirection2D.Horizontal, 0, groundLayer);
    }
}
