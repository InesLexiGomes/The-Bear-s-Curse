using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerMovement : MonoBehaviour
{
    // General variables
    [SerializeField] private int defaultGravity;
    private Rigidbody2D rb;

    // Variables for movement
    [SerializeField] private int speed;
    private Vector2 currentVelocity;
    private float deltaX;

    // Variables for jumping
    [SerializeField] private int jumpSpeed;
    [SerializeField] private float maxJumpTime;
    private float jumpTime;
    
    // Variables for IsGrounded method
    [SerializeField] private Transform groundCheck;
    [SerializeField] private int groundCheckRadius;
    [SerializeField] private LayerMask groundCheckLayers;

    // Variables for the Shoot method
    [SerializeField] private Transform bowPoint;
    [SerializeField] private GameObject arrow;
    private int arrowAmount = 10;

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

        // When you press the fire button you shoot
        if (Input.GetButtonUp("Fire1")) Shoot();
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

    private void Shoot()
    {
        // When the player is standing still and has equal to or more than 1 arrow they can shoot
        if (rb.velocity.x == 0 && arrowAmount >= 1)
        {
            // Creates instance of prefab
            Instantiate(arrow, bowPoint);
        }
        else Debug.Log("Can't shoot right now.");
    }
}
