using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class House : MonoBehaviour
{
    public GameObject Dialog;
    // Start is called before the first frame update
    void Awake()
    {
        Dialog = GameObject.Find("Canvas").transform.Find("Dialog").gameObject;
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if(other.tag=="player")
        {
            if(Input.GetKeyDown(KeyCode.E))
            {
                Debug.Log(1);
                SceneManager.LoadScene("Badend");
            }
            Dialog.SetActive(true);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if(other.tag=="player")
        {
            Dialog.SetActive(false);
        }
    }
}
