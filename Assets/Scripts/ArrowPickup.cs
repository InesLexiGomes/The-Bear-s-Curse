using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowPickup : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Increments arrow value
        PlayerManager.Instance.PickUpArrow();
        // Destroys object
        Destroy(gameObject);
    }
}
