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
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void QuitGame()
    {
        Application.Quit();
    }

    public void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            if (uiToToggle.activeInHierarchy) uiToToggle.SetActive(false);
            else uiToToggle.SetActive(true);
        }
    }
}
