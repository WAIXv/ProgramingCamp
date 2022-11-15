using Assets;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;


public static class IRENE
{
    public class IDLE : EntityStates.IState
    {
        private irene_ctrl Obj;
        public IDLE(irene_ctrl obj)
        {
            Obj = obj;
        }

        public void OnEnter() { }

        public void OnLeave() { }

        public void OnUpdate()
        {
            Vector3 vec = Obj.gameObject.transform.localScale;
            Obj.animator.SetBool("walk", true);
            if (Obj.rb.velocity.x >= 0.001f)
                vec.x = math.abs(vec.x);
            else if (Obj.rb.velocity.x <= -0.001f)
                vec.x = -math.abs(vec.x);
            else
                Obj.animator.SetBool("walk", false);

            Obj.gameObject.transform.localScale = vec;
        }
    }

    public class MOVE : EntityStates.IState
    {
        private irene_ctrl Obj;
        public MOVE(irene_ctrl obj)
        {
            Obj = obj;
        }

        public void OnEnter() { }

        public void OnLeave() { }

        public void OnUpdate()
        {

        }

    }


}


public class irene_ctrl : MonoBehaviour
{
    public EntityStates.IState EState;
    public Rigidbody2D rb;
    public Animator animator;

    private Vector3 last_pos;



    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        EState = new IRENE.IDLE(this);
        last_pos = gameObject.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        EState.OnUpdate();



    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "player")
        {
            //EState = new S.LOCKED(this, collision.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name == "player")
        {
            //EState = new S.IDLE(this);
        }
    }
}
