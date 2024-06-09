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

    // Variables for IsGrabbed method
    [SerializeField] private AudioSource boxaudio;
    [SerializeField] private Transform playerCheckF;
    [SerializeField] private Transform playerCheckB;
    [SerializeField] private int playerCheckRadius;
    [SerializeField] private LayerMask playerCheckLayers;
    private Vector2 currentPosition;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        boxaudio.loop = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (IsGrounded() && !IsGrabbed())
        {
            rb.bodyType = RigidbodyType2D.Static;
        }
        else rb.bodyType = RigidbodyType2D.Dynamic;

        if (!IsGrabbed()) boxaudio.Stop();
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
        // Sets default Value
        currentPosition.x = rb.transform.position.x;

        // Checks for an overlap in the playerCheck and Player Colliders
        Collider2D playerCollider = Physics2D.OverlapCircle(playerCheckF.position, playerCheckRadius, playerCheckLayers);

        if (playerCollider != null && Input.GetButton("Fire2"))
        {
            currentPosition.x = playerCollider.transform.position.x - 32;
        }
        // Checks for an overlap on the other side
        else
        {
            playerCollider = Physics2D.OverlapCircle(playerCheckB.position, playerCheckRadius, playerCheckLayers);
            if (playerCollider != null && Input.GetButton("Fire2"))
            {
                currentPosition.x = playerCollider.transform.position.x + 32;
            }
        }

        if (playerCollider != null && Input.GetButtonDown("Fire2")) boxaudio.Play();

        rb.transform.position = new Vector2(currentPosition.x, rb.transform.position.y);


        return (playerCollider != null && Input.GetButton("Fire2"));
    }
    private void OnDrawGizmosSelected()
    {
        if (groundCheck != null)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawSphere(groundCheck.position, groundCheckRadius);
            Gizmos.DrawSphere(playerCheckF.position, playerCheckRadius);
            Gizmos.DrawSphere(playerCheckB.position, playerCheckRadius);
        }
    }
}
