using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lever : MonoBehaviour
{
    [SerializeField] GameObject gate;
    private bool gateDestroyed = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!gateDestroyed && collision.gameObject.GetComponent<Player>() != null)
        {
            Debug.Log("Player collided with lever.");
            Destroy(gate);
            gateDestroyed = true;
        }
    }
}
