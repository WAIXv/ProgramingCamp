using UnityEngine;

public class EnterDialog : MonoBehaviour
{
    public GameObject enterdialog;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag== "Player")
        {
            enterdialog.SetActive(true);
        }
    }
    private void OnTriggerExit2D(Collider2D collision) 
    {
        if (collision.tag == "Player")
        {
            enterdialog.SetActive(false);
        }
    }
}
