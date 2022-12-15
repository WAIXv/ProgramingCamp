using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SwitchScene : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]private bool isDoor;
    [SerializeField] private bool isEnd;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if(isDoor)SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            else if(isEnd)SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex-2);
        }
    }

}
