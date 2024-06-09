using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour
{
    private Rigidbody2D rb;

    // Variables for IsGrounded method
    [SerializeField] private Transform groundCheck;
    [SerializeField] private int groundCheckRadius;
    [SerializeField] private LayerMask groundCheckLayers;
    [SerializeField] private Transform ropeCheck;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (IsGrounded() && !IsGrabbed())
        {
            rb.bodyType = RigidbodyType2D.Static;
        }
        else rb.bodyType = RigidbodyType2D.Dynamic;
    }
    private bool IsGrounded()
    {
        // Checks for an overlap in the groundCheck and Ground Colliders
        Collider2D groundCollider = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundCheckLayers);
        Collider2D ropeCollider = Physics2D.OverlapCircle(ropeCheck.position, groundCheckRadius, groundCheckLayers);

        // If the collider exists (isn't null) returns true, otherwise returns false
        return (groundCollider != null | ropeCollider != null);
    }

    private bool IsGrabbed()
    {
        return false;
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
