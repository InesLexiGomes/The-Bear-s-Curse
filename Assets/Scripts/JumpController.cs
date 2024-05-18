using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpController : MonoBehaviour
{
    Rigidbody2D rb;
    [SerializeField] int JumpPower;

    public Transform groundCheck;
    public LayerMask groundLayer;
    bool isGrounded;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics2D.OverlapCapsule(groundCheck.position,new Vector2(5f,20f), CapsuleDirection2D.Horizontal,0,groundLayer);

        if(Input.GetKeyDown("up") && isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, JumpPower);
        }
    }
}
