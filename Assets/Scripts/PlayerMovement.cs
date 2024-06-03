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
        // sets currentVelocity to the velocity of the player at that instant as a "default"
        currentVelocity = rb.velocity;

        // Determines velocity based on a fixed speed value and the horizontal movement input
        deltaX = Input.GetAxis("Horizontal");
        currentVelocity.x = deltaX * speed;

        // While the player is grounded they can jump
        if ((Input.GetButtonDown("Jump")) && IsGrounded())
        {
            currentVelocity.y = jumpSpeed;
            rb.gravityScale = 1.0f;
            jumpTime = Time.time;
        }
        // Add a bit more to the jump if the player holds the jump button after jumping
        else if ((Input.GetButton("Jump")) && ((Time.time - jumpTime) < maxJumpTime))
        {
            rb.gravityScale = 1.0f;
        }
        // Revert gravity to default
        else rb.gravityScale = defaultGravity;

        // Finally convert the currentVelocity into the velocity of the player
        rb.velocity = currentVelocity;

        // When walking backwards flip
        if (rb.velocity.x < 0) transform.rotation = Quaternion.Euler(0, 180, 0);
        // When walking forwards unflip
        else if (rb.velocity.x > 0) transform.rotation = Quaternion.identity;
    }

    private bool IsGrounded()
    {
        // Checks for an overlap in the groundCheck and Ground Colliders
        Collider2D collider = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundCheckLayers);

        // If the collider exists (isn't null) returns true, otherwise returns false
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
