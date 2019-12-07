using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonBehaviour : MonoBehaviour
{
    //MainMenu 0
    //GameScene 1
    //GameOverScene 2
    public void StartClick()
    {
        SceneManager.LoadScene(1);
    }

    public void MainMenuClick()
    {
        SceneManager.LoadScene(0);
    }

    //this could be in Playerscript
    public void GameOver()
    {
        SceneManager.LoadScene(2);
    }

    public void QuitClick()
    {
        Application.Quit();
    }
}
