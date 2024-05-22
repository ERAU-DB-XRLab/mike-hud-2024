using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.XR;

public class MIKEScreenManager : MonoBehaviour
{

    public static MIKEScreenManager Main;

    [SerializeField] private Transform canvas;
    [SerializeField] private TrackedPoseDriver bodyOrigin;
    [SerializeField] private float forwardOffset, verticalOffset, verticalPosOffset;

    private Transform mainCamera;

    void Awake()
    {
        Main = this;
        mainCamera = Camera.main.transform;
    }

    // Update is called once per frame
    void Update()
    {

        bodyOrigin.transform.position = mainCamera.transform.position + new Vector3(0, verticalPosOffset, 0);

        Vector3 forwardDir = transform.forward;
        forwardDir.y = 0;
        forwardDir.Normalize();

        canvas.transform.position = transform.position + (forwardOffset * forwardDir) + (verticalOffset * Vector3.up);
        canvas.transform.rotation = Quaternion.LookRotation(canvas.transform.position - transform.position, Vector3.up);
    
    }

}