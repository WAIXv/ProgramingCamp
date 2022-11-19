using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UiManager : MonoBehaviour
{

    public void return_0()
    {
        SceneManager.LoadScene("Level1");
    }
    public void exit()
    {
        Application.Quit();
    }
}
