using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    // The Prefabs that will be instantiated
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private GameObject bearPrefab;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void SwitchPlayerBear()
    {
        // Looks in the scene to find if there is a gameObject with the Player or Bear scripts
        GameObject player = FindObjectOfType<Player>().gameObject;
        GameObject bear = FindObjectOfType<Bear>().gameObject;

        // If there is a player it will instantiate the bearPrefab on the player's position
        if (player != null)
        {
            Instantiate(bearPrefab, player.transform.position, player.transform.rotation);
            Destroy(player);
        }
        // If there is a bear it will instantiate the playerPrefab on the bear's position
        else if (bear != null)
        {
            Instantiate(playerPrefab, bear.transform.position, bear.transform.rotation);
            Destroy(bear);
        }
    }
}
