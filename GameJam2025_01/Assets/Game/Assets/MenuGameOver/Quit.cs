using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Quit : MonoBehaviour
{
    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }
    public void Rules()
    {
        SceneManager.LoadScene(1);
    }

    public void Play()
    {
        SceneManager.LoadScene(2);
    }

    public void GameOver()
    {
        SceneManager.LoadScene(3);
    }

    public void EndApplication()
    {
        Application.Quit();
    }
}
