using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public GameObject pauseUI;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            pauseUI.SetActive(!pauseUI.activeSelf);
            if (pauseUI.activeSelf) Time.timeScale = 0;
            else Time.timeScale = 1;
        }
    }
}
