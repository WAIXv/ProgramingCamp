using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class mainMenu : MonoBehaviour
{
    public void Playgame()
    {
        SceneManager.LoadScene("Game");
    //  SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
    }
    public void Quitgame()
    {
        Application.Quit();
    }

}
