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
        // Looks in the scene to find if there is a gameObject with the Player or Bear scripts
        Player player = FindObjectOfType<Player>();
        Bear bear = FindObjectOfType<Bear>();

        // Code to set the checkpoint will go here
        if (player != null)
        {
            PlayerManager.Instance.SetPlayerCoordsAtCheckpoint(player.gameObject.transform.position);
            // Sets the ammount of arrows the player had at the checkpoint
            PlayerManager.Instance.SetArrowsAtCheckpoint(PlayerManager.Instance.ArrowCount);
            // After setting the place where player will respawn the object gets destroyed 
            Destroy(gameObject);
        }
        if (bear != null)
        {
            PlayerManager.Instance.SetPlayerCoordsAtCheckpoint(bear.gameObject.transform.position);
            // Sets the ammount of arrows the player had at the checkpoint
            PlayerManager.Instance.SetArrowsAtCheckpoint(PlayerManager.Instance.ArrowCount);
            // After setting the place where player will respawn the object gets destroyed 
            Destroy(gameObject);
        }
    }
}
