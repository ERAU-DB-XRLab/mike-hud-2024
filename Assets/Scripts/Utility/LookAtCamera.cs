using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtCamera : MonoBehaviour
{
    [SerializeField] private bool ignoreY = false;
    private Transform mainCamera;

    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main.transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (ignoreY)
        {
            transform.forward = Vector3.ProjectOnPlane(transform.position - mainCamera.position, Vector3.up);
        }
        else
        {
            transform.forward = transform.position - mainCamera.position;
        }
    }
}
