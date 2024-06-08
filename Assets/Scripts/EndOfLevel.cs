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
        // Sets the checkpoint to the next level's spawnpoint so when loading the player starts there
        PlayerManager.Instance.SetPlayerCoordsAtCheckpoint(playerSpawn);
        // Loads the next level
        SceneManager.LoadScene(nextLevel);
    }
}
