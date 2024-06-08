using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class Player : MonoBehaviour
{
    // General variables
    [SerializeField] private int defaultGravity;
    private Rigidbody2D rb;
    private Animator animator;

    // Variables for movement
    [SerializeField] private int speed;
    [SerializeField] private float sprintMultiplier;
    private Vector2 currentVelocity;
    private float deltaX;

    // Variables for jumping
    [SerializeField] private int jumpSpeed;
    [SerializeField] private float maxJumpTime;
    private float jumpTime;

    // Variables for IsClimbing method
    [SerializeField] private Transform climbCheck;
    [SerializeField] private int climbCheckRadius;
    [SerializeField] private LayerMask climbLayers;
    private float deltaY;

    // Variables for IsGrounded method
    [SerializeField] private Transform groundCheck;
    [SerializeField] private int groundCheckRadius;
    [SerializeField] private LayerMask groundCheckLayers;

    // Variables for the Shoot method
    [SerializeField] private Camera mainCamera;
    [SerializeField] private Transform bowPoint;
    [SerializeField] private GameObject arrow;
    private int arrowAmount = 10;

    // Variables for the bear form
    private bool isBear = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        // sets currentVelocity to the velocity of the player at that instant as a "default"
        currentVelocity = rb.velocity;

        // Determines velocity based on a fixed speed value and the horizontal movement input
        deltaX = Input.GetAxis("Horizontal");
        currentVelocity.x = deltaX * speed;
        if (Input.GetButton("Sprint")) currentVelocity.x *= sprintMultiplier;

        // Only allows verticall movement when climbing
        if (IsClimbing())
        {
            deltaY = Input.GetAxis("Vertical");
            currentVelocity.y = deltaY * speed;
        }

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

        // When you press the fire button and you're not in bear form you shoot
        if (Input.GetButtonUp("Fire1") && !isBear) Shoot();

        // Animation
        animator.SetFloat("AbsVelocityX", Mathf.Abs(currentVelocity.x)/100);
        animator.SetFloat("VelocityX", currentVelocity.x);
        animator.SetFloat("VelocityY", currentVelocity.y);
        animator.SetBool("IsJumping", !IsGrounded() && !IsClimbing());
        animator.SetBool("IsClimbing", IsClimbing());
        animator.SetBool("IsShooting", Input.GetButton("Fire1"));
        animator.SetBool("IsGrabbing", false);
    }

    private bool IsGrounded()
    {
        // Checks for an overlap in the groundCheck and Ground Colliders
        Collider2D collider = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundCheckLayers);

        // If the collider exists (isn't null) returns true, otherwise returns false
        return (collider != null);
    }

    private bool IsClimbing()
    {
        // Checks for an overlap in the groundCheck and Ground Colliders
        Collider2D collider = Physics2D.OverlapCircle(climbCheck.position, climbCheckRadius, climbLayers);

        // If the collider exists (isn't null) returns true, otherwise returns false
        return (collider != null);
    }

    private void Shoot()
    {
        // When the player is standing still and has equal to or more than 1 arrow they can shoot
        if (rb.velocity.x == 0 && arrowAmount >= 1)
        {
            // Transform mouse position to world position
            Vector2 mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);

            // Sets target from mouse position and the spawn point for the arrow
            Vector2 target = new Vector3(mousePos.x - bowPoint.position.x, mousePos.y - bowPoint.position.y);

            // Converts target vector to a rotation
            float rotation = Mathf.Atan2(target.x, target.y) * 180 / Mathf.PI;

            // Creates instance of prefab
            Instantiate(arrow, bowPoint.position, Quaternion.Euler(0, 0, -rotation));

            arrowAmount--;
        }
        else Debug.Log("Can't shoot right now.");
    }

    private void BearAttack()
    {

    }

    private void OnDrawGizmosSelected()
    {
        if (groundCheck != null)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawSphere(groundCheck.position, groundCheckRadius);
            Gizmos.DrawSphere(climbCheck.position, climbCheckRadius);
        }
    }
}
