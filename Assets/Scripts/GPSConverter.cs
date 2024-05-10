using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

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

    private void FindMetersPerLat(float lat) // Compute lengths of degrees
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

    public Vector3 LatLontoUCS(double lat, double lon)
    {
        FindMetersPerLat(_LatOrigin);
        double zPosition = metersPerLat * (lat - _LatOrigin); //Calc current lat
        double xPosition = metersPerLon * (lon - _LonOrigin); //Calc current lat
        return new Vector3((float)xPosition, 0, (float)zPosition);
    }

    public LatLonCoords UTMToLatLon(double utmX, double utmY, string utmZone)//, out double latitude, out double longitude)
    {
        bool isNorthHemisphere = utmZone.Last() >= 'N';

        var diflat = -0.00066286966871111111111111111111111111;
        var diflon = -0.0003868060578;

        var zone = int.Parse(utmZone.Remove(utmZone.Length - 1));
        var c_sa = 6378137.000000;
        var c_sb = 6356752.314245;
        var e2 = Math.Pow((Math.Pow(c_sa, 2) - Math.Pow(c_sb, 2)), 0.5) / c_sb;
        var e2cuadrada = Math.Pow(e2, 2);
        var c = Math.Pow(c_sa, 2) / c_sb;
        var x = utmX - 500000;
        var y = isNorthHemisphere ? utmY : utmY - 10000000;

        var s = ((zone * 6.0) - 183.0);
        var lat = y / (c_sa * 0.9996);
        var v = (c / Math.Pow(1 + (e2cuadrada * Math.Pow(Math.Cos(lat), 2)), 0.5)) * 0.9996;
        var a = x / v;
        var a1 = Math.Sin(2 * lat);
        var a2 = a1 * Math.Pow((Math.Cos(lat)), 2);
        var j2 = lat + (a1 / 2.0);
        var j4 = ((3 * j2) + a2) / 4.0;
        var j6 = ((5 * j4) + Math.Pow(a2 * (Math.Cos(lat)), 2)) / 3.0;
        var alfa = (3.0 / 4.0) * e2cuadrada;
        var beta = (5.0 / 3.0) * Math.Pow(alfa, 2);
        var gama = (35.0 / 27.0) * Math.Pow(alfa, 3);
        var bm = 0.9996 * c * (lat - alfa * j2 + beta * j4 - gama * j6);
        var b = (y - bm) / v;
        var epsi = ((e2cuadrada * Math.Pow(a, 2)) / 2.0) * Math.Pow((Math.Cos(lat)), 2);
        var eps = a * (1 - (epsi / 3.0));
        var nab = (b * (1 - epsi)) + lat;
        var senoheps = (Math.Exp(eps) - Math.Exp(-eps)) / 2.0;
        var delt = Math.Atan(senoheps / (Math.Cos(nab)));
        var tao = Math.Atan(Math.Cos(delt) * Math.Tan(nab));

        //longitude = ((delt * (180.0 / Math.PI)) + s) + diflon;
        //latitude = ((lat + (1 + e2cuadrada * Math.Pow(Math.Cos(lat), 2) - (3.0 / 2.0) * e2cuadrada * Math.Sin(lat) * Math.Cos(lat) * (tao - lat)) * (tao - lat)) * (180.0 / Math.PI)) + diflat;

        LatLonCoords coords = new LatLonCoords();
        coords.lat = ((lat + (1 + e2cuadrada * Math.Pow(Math.Cos(lat), 2) - (3.0 / 2.0) * e2cuadrada * Math.Sin(lat) * Math.Cos(lat) * (tao - lat)) * (tao - lat)) * (180.0 / Math.PI)) + diflat;
        coords.lon = ((delt * (180.0 / Math.PI)) + s) + diflon;

        return coords;
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

public struct LatLonCoords
{
    public double lat;
    public double lon;
}