using UnityEngine;

public class Sign : MonoBehaviour
{
    public GameObject theDialog;
    [SerializeField] private Animator DialogAnmi;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            theDialog.SetActive(true);
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            DialogAnmi.Play("DiaExit");
        }
    }
}