using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cherry1 : MonoBehaviour
{
    [SerializeField] private PlayerCtrler playerCtrler;
    [SerializeField] private Collider2D colCherry;
    [SerializeField] private Animator animCherry;
    // Start is called before the first frame update
    void Start()
    {
        animCherry=GetComponent<Animator>();
        colCherry=GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            AudioManager.instance.PlayFeedbackSound();
            animCherry.Play("Feedback");
        }
    }
    private void Feedback()
    {
        Destroy(this.gameObject);
        playerCtrler.ScorePlus();
    }
}
