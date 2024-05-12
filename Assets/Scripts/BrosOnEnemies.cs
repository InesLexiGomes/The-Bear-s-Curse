using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrosOnEnemies : MonoBehaviour
{
    [SerializeField]
    private Rigidbody2D rb;
    public BroMechanic bro;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (bro.Brothers == true)
        {
            rb.velocity = new Vector2(rb.velocity.x + 100, rb.velocity.y);
        }
    }
}
