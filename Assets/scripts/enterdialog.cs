using UnityEngine;

public class enterdialog : MonoBehaviour
{
    public GameObject enterDiolog;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            enterDiolog.SetActive(true);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            enterDiolog.SetActive(false);
        }
    }
}
