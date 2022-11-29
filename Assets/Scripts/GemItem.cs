using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemItem : Collection
{
    // Start is called before the first frame update
    void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        base.Update();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            GemUI.CurrentGemNum++;
        }
    }
}
