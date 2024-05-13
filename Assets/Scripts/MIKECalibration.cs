using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MIKECalibration : MonoBehaviour
{
    [SerializeField] private MIKEMap map;
    [SerializeField] private Transform mapParent;

    private Vector3 astronautPosition;
    private float astronautHeading;

    // Start is called before the first frame update
    void Start()
    {
        TSSManager.Main.OnIMUUpdated += UpdateIMU;
    }

    private void UpdateIMU(IMUData data)
    {
        astronautPosition = map.GetPositionFromUTM(data.OtherEVA.posx, data.OtherEVA.posy, false);
        astronautHeading = (float)data.YourEVA.heading;
    }

    public void Calibrate()
    {
        mapParent.transform.eulerAngles = new Vector3(0f, -astronautHeading, 0f);
        mapParent.transform.position = mapParent.transform.TransformPoint(Camera.main.transform.position - astronautPosition);
    }
}
