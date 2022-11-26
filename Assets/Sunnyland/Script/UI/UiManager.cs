using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class UiManager : MonoBehaviour
{
    public GameObject pauseMenu;
    public Animator pauseAnim;
    public AudioMixer audioMixer;

    public void GoToLevel1()
    {
        SceneManager.LoadScene("Level1");
    }
    public void exit()
    {
        Application.Quit();
    }
    public void PauseGame()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        pauseAnim.enabled = false;
    }

    public void ResumeGame()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1.0f;
        pauseAnim.enabled = true;
    }
    public void SetVolume(float value)
    {
        audioMixer.SetFloat("MainVolume",value);
    }
}
