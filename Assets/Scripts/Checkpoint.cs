using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    /// <summary>
    /// Sets the place where player will respawn, how many arrows they had and then destroys the checkpoint
    /// </summary>
    /// <param name="collider"> Will be player as it is the only one allowed to interact with it </param>
    private void OnTriggerEnter2D(Collider2D collider)
    {
        // Code to set the checkpoint will go here

        // Sets the ammount of arrows the player had at the checkpoint
        PlayerManager.Instance.SetArrowsAtCheckpoint(PlayerManager.Instance.ArrowCount);

        // After setting the place where player will respawn the object gets destroyed 
        Destroy(gameObject);
    }
}
