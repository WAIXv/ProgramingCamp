using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ESC_Menu : MonoBehaviour
{   
    [SerializeField]private weapon_control controlWeapon;
    [SerializeField]private bool isStop=false;
    [SerializeField]private GameObject menu;
    [SerializeField]private audio_manager audioSrc;
    [SerializeField]public bool ifSetting;

    /// Awake is called when the script instance is being loaded.
    /// </summary>
    private void Awake()
    {
        ifSetting = false;
        audioSrc = GameObject.Find("AudioManager").GetComponent<audio_manager>();
        controlWeapon = GameObject.Find("player").GetComponent<weapon_control>();
    }
    void Update()
    {
        if(!isStop)
        {
            if(Input.GetKeyDown(KeyCode.Escape)&&!ifSetting)
            {
                audioSrc.Pause();
                controlWeapon.able_shoot=false;
                menu.SetActive(true);
                isStop = !isStop;
                Time.timeScale = 0;
            }
        }
        else
        {
            if(Input.GetKeyDown(KeyCode.Escape)&&!ifSetting)
            {
                audioSrc.Play();
                controlWeapon.able_shoot=true;
                Debug.Log("sss");
                menu.SetActive(false);
                Time.timeScale = 1;
                isStop = !isStop;
            }
        }
    }
    public void backGame()
    {
        controlWeapon.able_shoot=true;
        audioSrc.Play();
        Debug.Log("sss");
        menu.SetActive(false);
        Time.timeScale = 1;
        isStop = !isStop;
    }

    public void backMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void quitGame()
    {
        Application.Quit();
    }

    public void isSetting()
    {
        ifSetting = ! ifSetting;
    }
}
