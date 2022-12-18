using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.UIElements;

public class EneryBoss : MonoBehaviour
{
    public float speed;
    public float radius;
    public float startTime;
    // Start is called before the first frame update
    private Transform playertransform;

    public Animator anim;
    public bool Begin=false;
    public int lenth;//ª÷∏¥ ±º‰
    private int cut=0;
    void Start()
    {
       // anim = GameObject.FindGameObjectWithTag("boss").GetComponent<Animator>();

        playertransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playertransform != null)
        {
            float distance = (transform.position - playertransform.position).sqrMagnitude;
            if (distance < radius)
            {
                cut = 0;
                if (anim.GetBool("wake") == false)
                {
                    StartCoroutine(StartWake());
                }
                if(Begin)
                {
                    transform.position = UnityEngine.Vector2.MoveTowards(transform.position, playertransform.position, speed * Time.deltaTime);
                }
            }
            else
            {
                cut++;
                if (cut >= lenth){
                    anim.SetBool("wake", false);
                    anim.SetBool("move", false);
                    Begin = false;
                }
            }
        }
    }

    IEnumerator StartWake()
    {
        anim.SetBool("wake", true);
        yield return new WaitForSeconds(startTime);//—” ±
        Begin = true;
        anim.SetBool("move", true);
    }
}
