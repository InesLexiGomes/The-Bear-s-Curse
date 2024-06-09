using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private int speed;
    [SerializeField] private int timeToTurn;
    private float timeNow;

    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        timeNow = timeToTurn;
    }

    // Update is called once per frame
    void Update()
    {
        if (timeNow <= 0)
        {
            speed = -speed;
            timeNow = timeToTurn;
            // When walking backwards flip
            if (speed < 0) transform.rotation = Quaternion.Euler(0, 180, 0);
            // When walking forwards unflip
            else if (speed > 0) transform.rotation = Quaternion.identity;
        }

        rb.velocity = new Vector2(speed, rb.velocity.y);
        timeNow -= Time.deltaTime;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // If it collides with a player or bear load the checkpoint
        if ((collision.gameObject.GetComponent<Player>() != null )|(collision.gameObject.GetComponent<Bear>())) PlayerManager.Instance.CheckpointLoad();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // If it collides with an arrow it "dies"
        if (collision.gameObject.GetComponent<Arrow>()) Destroy(gameObject);
    }
}
