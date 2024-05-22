using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MIKECalibration : MonoBehaviour
{
    [SerializeField] private MIKEMap map;
    [SerializeField] private Transform mapParent;
    [SerializeField] private Transform mapRotate;
    [SerializeField] private Transform mapCorrection;
    [SerializeField] private Transform failsafe;

    [Header("This should be the main camera in most cases")]
    [SerializeField] private Transform rotateSnap;

    private float easting, northing;
    private float heading;

    // Start is called before the first frame update
    void Start()
    {
        TSSManager.Main.OnIMUUpdated += UpdateIMU;
    }

    private void UpdateIMU(IMUData data)
    {
        easting = 298305;
        northing = 3272330;
        heading = 90f;
    }

    public void Calibrate(bool useFailsafe = false)
    {
        mapCorrection.rotation = Quaternion.Euler(0f, rotateSnap.eulerAngles.y, 0f);
        mapRotate.forward = -Vector3.ProjectOnPlane(rotateSnap.forward, Vector3.up);
        Vector3 astronautPosition = map.GetPositionFromUTM(easting, northing, false);
        mapParent.position = rotateSnap.position - (useFailsafe ? failsafe.position : astronautPosition);
    }
}
