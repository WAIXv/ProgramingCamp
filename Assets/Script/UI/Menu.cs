using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    [SerializeField] private bool isShow;
    public GameObject PauseMenu;
    private void Start()
    {
        PauseMenu.SetActive(false);
        isShow = false;
    }
    private void Update()
    {

        SettingMenu();
    }
    public void PlayGame()
    {
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex+1);
    }
    public void ExitGame()
    {
        Application.Quit();
    }
    public void UIEnable()
    {
        GameObject.Find("Canvas/MainMenu/UI").SetActive(true);
    }
    public void PauseGame()
    {
        PauseMenu.SetActive(true);
        isShow = false;
        Time.timeScale = 0f;
    }
    public void ReturnGame()
    {
        PauseMenu.SetActive(false);
        isShow = true;
        Time.timeScale = 1f;
    }
    public void SettingMenu()
    {
        if (isShow)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                PauseMenu.SetActive(true);
                isShow = false;
                Time.timeScale = 0f;
            }
        }
        else if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseMenu.SetActive(false);
            isShow = true;
            Time.timeScale = 1f;
        }
    }
}
