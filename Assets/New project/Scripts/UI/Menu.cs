using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    [SerializeField]private GameObject pauseMenu;
    [SerializeField] private GameObject player;
    [SerializeField] private PlayerCtrler playerCtrler;
    private void Start()
    {
        playerCtrler=player.gameObject.GetComponent<PlayerCtrler>();
    }
    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void QuitGame()
    {
        Application.Quit();
        //UnityEditor.EditorApplication.isPlaying = false;
    }
    public void PauseGame()
    {
        pauseMenu.SetActive(true);
        playerCtrler.enabled = false;
        Time.timeScale = 0f;
    }
    public void ResumeGame()
    {
        pauseMenu.SetActive(false);
        playerCtrler.enabled = true;
        Time.timeScale = 1f;
    }
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1f;
    }
}
