using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenu : MonoBehaviour
{
    /// <summary>
    /// Função que faz o botão "Play" levar ao nível 1
    /// </summary>
    public void PlayGame()
    {
        SceneManager.LoadScene(1);
    }
    /// <summary>
    /// Função que faz o botão "Leave" fechar o jogo
    /// </summary>
    public void QuitGame()
    {
        Application.Quit();
    }
}
