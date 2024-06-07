using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// Sets the place where player will respawn and then destroys the checkpoint
    /// </summary>
    /// <param name="collider"> Will be player as it is the only one allowed to interact with it </param>
    private void OnTriggerEnter2D(Collider2D collider)
    {
        // Code to set the checkpoint will go here

        // After setting the place where player will respawn the object gets destroyed 
        Destroy(gameObject);
    }
}
