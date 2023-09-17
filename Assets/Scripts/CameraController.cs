using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target;
    public Vector3 targetOfset;
    public bool isFinal;
    public GameObject FinalTarget;
    private void Start()
    {
        targetOfset= transform.position-target.position;
    }

    private void LateUpdate()
    {
        if(!isFinal )
            transform.position = Vector3.Lerp(transform.position,target.position+targetOfset,0.125f);
        else
            transform.position = Vector3.Lerp(transform.position, FinalTarget.transform.position + targetOfset, 0.125f);

    }
}
