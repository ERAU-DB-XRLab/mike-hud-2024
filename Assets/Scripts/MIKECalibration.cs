using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MIKECalibration : MonoBehaviour
{
    [SerializeField] private MIKEMap map;
    [SerializeField] private Transform mapParent;

    private float astronautHeading;

    private float defaultEasting = 298355;
    private float defaultNorthing = 3272383;

    private Vector3 middlePos;
    private Vector2 astronautPos;

    // Start is called before the first frame update
    void Start()
    {
        middlePos = new Vector2(defaultEasting, defaultNorthing);
        TSSManager.Main.OnIMUUpdated += UpdateIMU;
    }

    void Update()
    {
        Debug.Log("BRUH: " + (new Vector3(middlePos.x, 0, middlePos.y) + GameObject.Find("XR Origin").transform.position));
    }

    private void UpdateIMU(IMUData data)
    {
        astronautPos = new Vector2(298405, 3272438);
        astronautHeading = (float)data.YourEVA.heading + 90;
        Debug.Log(data.YourEVA.posx + " " + data.YourEVA.posy);
    }

    public void Calibrate()
    {
        mapParent.transform.position = new Vector3(middlePos.x - astronautPos.x, 0, middlePos.y - astronautPos.y);
    }
}
