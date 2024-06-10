using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndOfLevel : MonoBehaviour
{
    // In the unity editor insert the name of the next scene and the player's spawn location
    [SerializeField] string nextLevel;
    [SerializeField] Vector2 playerSpawn;


    private void OnTriggerEnter2D(Collider2D collider)
    {
        // Checks if player has the key first
        if (PlayerManager.Instance.hasKey)
        {
            // Sets the checkpoint to the next level's spawnpoint so when loading the player starts there
            PlayerManager.Instance.SetPlayerCoordsAtCheckpoint(playerSpawn);
            // Sets the ammount of arrows for the next Level
            PlayerManager.Instance.SetArrowsAtCheckpoint(PlayerManager.Instance.ArrowCount);
            // Loads the next level
            SceneManager.LoadScene(nextLevel);
        }
    }     
}
