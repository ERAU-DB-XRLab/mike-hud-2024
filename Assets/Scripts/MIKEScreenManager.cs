using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MIKEScreenManager : MonoBehaviour
{

    [SerializeField] private Transform canvas;
    [SerializeField] private float forwardOffset, verticalOffset;

    // Update is called once per frame
    void Update()
    {

        Vector3 forwardDir = transform.forward;
        forwardDir.y = 0;
        forwardDir.Normalize();

        canvas.transform.position = transform.position + (forwardOffset * forwardDir) + (verticalOffset * Vector3.up);
        canvas.transform.rotation = Quaternion.LookRotation(canvas.transform.position - transform.position, Vector3.up);
    
    }

}