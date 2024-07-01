using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
    public void StartGame1()
    {
        SceneManager.LoadScene(22);
    }
    public void StartGame2()
    {
        SceneManager.LoadScene(23);
    }
    public void StartGame3()
    {
        SceneManager.LoadScene(0);
    }
    public void StartGame4()
    {
        SceneManager.LoadScene(1);
    }
    public void StartGame5()
    {
        SceneManager.LoadScene(2);
    }
    public void StartGame6()
    {
        SceneManager.LoadScene(14);
    }
    public void StartGame7()
    {
        SceneManager.LoadScene(15);
    }
    public void StartGame8()
    {
        SceneManager.LoadScene(17);
    }
    public void StartGame9()
    {
        SceneManager.LoadScene(18);
    }
    public void StartGameA()
    {
        SceneManager.LoadScene(19);
    }
    public void StartGamefinal()
    {
        SceneManager.LoadScene(28);
    }
    public void StartGameExit()
    {
        // Salir del juego
        Application.Quit();

        // Mensaje de consola para verificar en el editor
        Debug.Log("Game is exiting");
    }
}
