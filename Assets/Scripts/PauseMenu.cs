using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] GameObject uiToToggle;
    public void Continue()
    {

    }
    public void Restart()
    {
        PlayerManager.Instance.CheckpointLoad();
    }
    public void QuitGame()
    {
        Application.Quit();
    }

    private void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            Pause();
        }
    }

    private void Pause()
    {
        // Find either the Player or the Bear script, whichever exists
        MonoBehaviour character = FindObjectOfType<Player>();
        if (character == null)
            character = FindObjectOfType<Bear>();

        if (uiToToggle.activeInHierarchy)
        {
            // Disable the UI
            uiToToggle.SetActive(false);
            // Re-enable the player
            character.enabled = true;
        }
        else
        {
            // Enable the UI
            uiToToggle.SetActive(true);
            // Disable the Player
            character.enabled = false;
        }
    }
}
