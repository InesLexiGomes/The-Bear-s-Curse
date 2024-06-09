using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    [SerializeField] private string sceneName;
    public void PlayGame()
    {
        SceneManager.LoadScene(sceneName);
    }

    public void Options()
    {
        SceneManager.LoadScene("Options");
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
