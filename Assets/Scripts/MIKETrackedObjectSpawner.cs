using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MIKETrackedObjectSpawner : MonoBehaviour
{
    [SerializeField] private MIKEMap map;
    [SerializeField] private GameObject otherAstronautPrefab;
    [SerializeField] private GameObject roverPrefab;
    [Space]
    [SerializeField] private float interpolationSpeed = 5f;

    private GameObject currOtherAstronaut;
    private GameObject currRover;

    private Vector3 otherAstronautNewLocalPosition;
    private Quaternion otherAstronautNewLocalRotation;

    private Vector3 roverNewLocalPosition;
    private Quaternion roverNewLocalRotation;

    // Start is called before the first frame update
    void Start()
    {
        TSSManager.Main.OnIMUUpdated += UpdateIMU;
        TSSManager.Main.OnRoverUpdated += UpdateRover;

        TSSManager.Main.OnDisconnected += OnDisconnection;
    }

    private void UpdateIMU(IMUData data)
    {
        if (currOtherAstronaut == null)
        {
            currOtherAstronaut = Instantiate(otherAstronautPrefab, map.transform);
        }

        Vector3 newPos = map.GetPositionFromUTM(data.OtherEVA.posx, data.OtherEVA.posy, true);
        otherAstronautNewLocalRotation = Quaternion.LookRotation(Vector3.ProjectOnPlane(newPos - otherAstronautNewLocalPosition, Vector3.up));
        otherAstronautNewLocalPosition = newPos;
    }

    private void UpdateRover(RoverData data)
    {
        if (currRover == null)
        {
            currRover = Instantiate(roverPrefab, map.transform);
        }

        Vector3 newPos = map.GetPositionFromUTM(data.posx, data.posy, true);
        roverNewLocalRotation = Quaternion.LookRotation(Vector3.ProjectOnPlane(newPos - roverNewLocalPosition, Vector3.up));
        roverNewLocalPosition = newPos;
    }

    private void OnDisconnection()
    {
        Destroy(currOtherAstronaut);
        Destroy(currRover);

        currOtherAstronaut = null;
        currRover = null;
    }

    // Update is called once per frame
    void Update()
    {
        if (currOtherAstronaut != null)
        {
            currOtherAstronaut.transform.localPosition = Vector3.Lerp(currOtherAstronaut.transform.localPosition, otherAstronautNewLocalPosition, Time.deltaTime * interpolationSpeed);
            currOtherAstronaut.transform.localRotation = Quaternion.Lerp(currOtherAstronaut.transform.localRotation, otherAstronautNewLocalRotation, Time.deltaTime * interpolationSpeed);
        }

        if (currRover != null)
        {
            currRover.transform.localPosition = Vector3.Lerp(currRover.transform.localPosition, roverNewLocalPosition, Time.deltaTime * interpolationSpeed);
            currRover.transform.localRotation = Quaternion.Lerp(currRover.transform.localRotation, roverNewLocalRotation, Time.deltaTime * interpolationSpeed);
        }
    }
}
