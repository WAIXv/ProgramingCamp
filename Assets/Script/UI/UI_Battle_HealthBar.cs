using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class UI_Battle_HealthBar : MonoBehaviour
{
    [SerializeField]
    private GameObject Internal;
    [SerializeField]
    private entity_Instance EI;

    private SpriteRenderer sr1;
    private SpriteRenderer sr2;
    //public float rate = 1;
    // Start is called before the first frame update
    void Start()
    {
        sr1 = gameObject.GetComponent<SpriteRenderer>();
        sr2 = Internal.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        float rate = math.min(math.max(EI.health / EI.getMaxHealth(), 0), 1.0f);
        if(rate >= 1.0f)
        {
            sr1.enabled = false;
            sr2.enabled = false;
        }
        else
        {
            sr1.enabled = true;
            sr2.enabled = true;
        }

        Vector3 tmp = Internal.transform.localScale;
        tmp.x = rate;
        Internal.transform.localScale = tmp;

        tmp = Internal.transform.localPosition;
        tmp.x = (rate - 1f) / 2;
        Internal.transform.localPosition = tmp;
    }
}
