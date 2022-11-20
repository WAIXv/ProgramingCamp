using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UiManager : MonoBehaviour
{

    public void GoToLevel1()
    {
        SceneManager.LoadScene("Level1");
    }
    public void exit()
    {
        Application.Quit();
    }
}
