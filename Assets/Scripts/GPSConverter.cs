using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GPSConverter : MonoBehaviour
{

    public static GPSConverter Main { get; private set; }

    //Reference - https://github.com/MichaelTaylor3D/UnityGPSConverter/blob/master/GPSEncoder.cs
    private float _LatOrigin = 0.0f;
    private float _LonOrigin = 0.0f;
    private float metersPerLat = 0.0f;
    private float metersPerLon = 0.0f;

    private void Awake()
    {
        Main = this;
        setLatOrigin(29.188302993774414f);
        setLonOrigin(-81.053047180175781f);
    }
    public void FindMetersPerLat(float lat) // Compute lengths of degrees
    {
        float m1 = 111132.92f;
        float m2 = -559.82f;
        float m3 = 1.175f;
        float m4 = -0.0023f;
        float p1 = 111412.84f;
        float p2 = -93.5f;
        float p3 = 0.118f;

        lat = lat * Mathf.Deg2Rad;

        // Calculate the length of a degree of latitude and longitude in meters
        metersPerLat = m1 + (m2 * Mathf.Cos(2 * (float)lat)) + (m3 * Mathf.Cos(4 * (float)lat)) + (m4 * Mathf.Cos(6 * (float)lat));
        metersPerLon = (p1 * Mathf.Cos((float)lat)) + (p2 * Mathf.Cos(3 * (float)lat)) + (p3 * Mathf.Cos(5 * (float)lat));
    }

    public Vector3 ConvertGPStoUCS(double gpsX, double gpsY)
    {
        FindMetersPerLat(_LatOrigin);
        double zPosition = metersPerLat * (gpsX - _LatOrigin); //Calc current lat
        double xPosition = metersPerLon * (gpsY - _LonOrigin); //Calc current lat
        return new Vector3((float)xPosition, 0, (float)zPosition);
    }

    public void setLatOrigin(float lat)
    {
        _LatOrigin = lat;
    }

    public void setLonOrigin(float lon)
    {
        _LonOrigin = lon;
    }

}