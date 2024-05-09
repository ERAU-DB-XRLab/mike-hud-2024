using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MIKEMapNavigationWidget : MIKEWidget
{

    [SerializeField] private Vector3 mapRot2D;
    [SerializeField] private Vector3 mapRot3D;
    [SerializeField] private float rotationSpeed;
    [SerializeField] private Rotate mapRotate;

    private Vector3 desiredRot;
    private bool rotating;

    void Update()
    {
        desiredRot = rotating ? new Vector3(mapRot3D.x, 0, mapRotate.transform.eulerAngles.z) : mapRot2D;
        mapRotate.transform.rotation = Quaternion.RotateTowards(mapRotate.transform.rotation, rotating ? Quaternion.Euler(mapRot3D) : Quaternion.Euler(desiredRot), rotationSpeed * Time.deltaTime);
    }

    public void ToggleMap()
    {
        rotating = !rotating;
        mapRotate.enabled = rotating;
    }

}