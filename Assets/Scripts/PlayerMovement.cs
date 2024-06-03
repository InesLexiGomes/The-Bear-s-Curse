using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private int speed;
    [SerializeField] private int jumpSpeed;
    [SerializeField] private float maxJumpTime;
    [SerializeField] private int defaultGravity;
    private float jumpTime;

    private Vector2 currentVelocity;
    private float deltaX;

    [SerializeField] private Transform groundCheck;
    [SerializeField] private int groundCheckRadius;
    [SerializeField] private LayerMask groundCheckLayers;

    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        currentVelocity = rb.velocity;

        deltaX = Input.GetAxis("Horizontal");

        currentVelocity.x = deltaX * speed;

        if ((Input.GetButtonDown("Jump")) && IsGrounded())
        {
            currentVelocity.y = jumpSpeed;
            rb.gravityScale = 1.0f;
            jumpTime = Time.time;
        }
        else if ((Input.GetButton("Jump")) && ((Time.time - jumpTime) < maxJumpTime))
        {
            rb.gravityScale = 1.0f;
        }
        else
        {
            rb.gravityScale = defaultGravity;
        }

        rb.velocity = currentVelocity;

        // When walking backwards flip
        if (rb.velocity.x < 0) transform.rotation = Quaternion.Euler(0, 180, 0);
        // When walking forwards unflip
        else if (rb.velocity.x > 0) transform.rotation = Quaternion.identity;
    }

    private void FixedUpdate()
    {
        if (Input.GetKeyDown("up") && IsGrounded())
        {
            rb.velocity = new Vector2(deltaX * speed, jumpSpeed);
        }
    }

    private bool IsGrounded()
    {
        Collider2D collider = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundCheckLayers);

        return (collider != null);
    }

    private void OnDrawGizmosSelected()
    {
        if (groundCheck != null)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawSphere(groundCheck.position, groundCheckRadius);
        }
    }
}
