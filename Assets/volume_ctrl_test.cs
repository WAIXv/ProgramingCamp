using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class volume_ctrl_test : MonoBehaviour
{
    [SerializeField]
    private Volume volume;
    [SerializeField]
    private ColorCurves colorCurves;
    [SerializeField]
    private float t= 0f;

    private TextureCurve tc;


    // Start is called before the first frame update
    void Start()
    {
        volume.profile.TryGet<ColorCurves>(out colorCurves);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
