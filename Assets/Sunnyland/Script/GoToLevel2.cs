using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.SceneManagement;

public class GoToLevel2 : MonoBehaviour
{
    public GameObject enterDialog;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            enterDialog.SetActive(true);
            //SceneManager.LoadScene("Level2");
        } 
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            enterDialog.SetActive(false);
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (Input.GetKey(KeyCode.E))
        {
            SceneManager.LoadScene("level2");
        }
    }

}
