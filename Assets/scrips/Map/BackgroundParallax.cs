using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundParallax : MonoBehaviour
{
    private Transform cameraTransform;
    private Vector3 lastCameraPositon;
    // Start is called before the first frame update
    [SerializeField] private Vector2 parallaxMultipl;
    void Start()
    {
        cameraTransform = Camera.main.transform;
        lastCameraPositon = cameraTransform.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 DeltaPosition = cameraTransform.position - lastCameraPositon;
        transform.position += new Vector3(parallaxMultipl.x*DeltaPosition.x,parallaxMultipl.y*DeltaPosition.y,0);
//        transform.position = new Vector3(DeltaPosition.x,DeltaPosition.y);
//        transform.position += DeltaPosition;
        lastCameraPositon = cameraTransform.position;
    }
}
