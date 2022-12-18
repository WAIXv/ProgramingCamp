using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
public class Menu : MonoBehaviour
{
    public GameObject pauseMenu;
    public GameObject player;
    public AudioMixer audioMixer;
    public void playGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void quitGame()
    {
        Application.Quit();
    }
    public void UIEnable()
    {
        GameObject.Find("Canvas/mainMenu/UI").SetActive(true);
    }
    public void PauseGame()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        player.GetComponent<AudioSource>().enabled = false;
    }
    public void ResumeGame()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        player.GetComponent<AudioSource>().enabled = true;
    }
    public void SetVolume(float value)
    {
        audioMixer.SetFloat("mainvolume", value);
    }
}
