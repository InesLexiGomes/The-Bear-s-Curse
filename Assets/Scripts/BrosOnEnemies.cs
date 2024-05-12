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
            rb.position = new Vector2(rb.position.x + 200 * Time.deltaTime, rb.position.y);
        }
    }
}
