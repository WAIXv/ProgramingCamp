using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ESC_Menu : MonoBehaviour
{   
    [SerializeField]private weapon_control controlWeapon;
    [SerializeField]private bool isStop=false;
    [SerializeField]private GameObject menu;

    /// Awake is called when the script instance is being loaded.
    /// </summary>
    private void Awake()
    {
        controlWeapon = GameObject.Find("player").GetComponent<weapon_control>();
    }
    void Update()
    {
        if(!isStop)
        {
            if(Input.GetKeyDown(KeyCode.Escape))
            {
                controlWeapon.able_shoot=false;
                menu.SetActive(true);
                isStop = !isStop;
                Time.timeScale = 0;
            }

        }
        else
        {
            if(Input.GetKeyDown(KeyCode.Escape))
            {
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
        Debug.Log("sss");
        menu.SetActive(false);
        Time.timeScale = 1;
        isStop = !isStop;
    }

    public void backMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
