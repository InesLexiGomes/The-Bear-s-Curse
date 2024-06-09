using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bear : MonoBehaviour
{
    // General variables
    [SerializeField] private int defaultGravity;
    private Rigidbody2D rb;
    private Animator animator;

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

    // Variables for Attack method
    [SerializeField] private Transform boxCheck;
    [SerializeField] private int boxCheckRadius;
    [SerializeField] private LayerMask boxCheckLayers;
    [SerializeField] private GameObject arrowPickupPrefab;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
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

        if (Input.GetButtonDown("Fire1")) Attack();

        // Animation
        animator.SetFloat("AbsVelocityX", Mathf.Abs(currentVelocity.x) / 100);
        //animator.SetFloat("VelocityY", currentVelocity.y);
        //animator.SetBool("IsJumping", !IsGrounded());
        animator.SetBool("IsAttacking", Input.GetButton("Fire1"));
    }

    private bool IsGrounded()
    {
        // Checks for an overlap in the groundCheck and Ground Colliders
        Collider2D collider = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundCheckLayers);

        // If the collider exists (isn't null) returns true, otherwise returns false
        return (collider != null);
    }
    
    private void Attack()
    {
        // Checks for an overlap in the boxCheck and Box colliders
        Collider2D collider = Physics2D.OverlapCircle(boxCheck.position, boxCheckRadius, boxCheckLayers);

        // If it finds a box it gets destroyed
        if (collider != null)
        {
            Instantiate(arrowPickupPrefab, collider.transform.position, collider.transform.rotation);
            Destroy(collider.gameObject);
        }
    }
}
