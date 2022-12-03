using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Badend : MonoBehaviour
{
    public static void back()
    {
        SceneManager.LoadScene("Mainmenu");
    }
}
