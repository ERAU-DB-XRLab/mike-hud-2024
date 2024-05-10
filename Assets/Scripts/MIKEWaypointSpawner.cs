using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MIKEWaypointSpawner : MonoBehaviour
{

    public static MIKEWaypointSpawner Main;

    [SerializeField] private GameObject waypointPrefab;
    [SerializeField] private MIKEMap map;
    [SerializeField] private GameObject bodyOrigin;

    private Dictionary<int, GameObject> waypoints = new Dictionary<int, GameObject>();

    void Awake()
    {
        if (Main == null)
            Main = this;
        else
            Destroy(this);
    }

    // Start is called before the first frame update
    void Start()
    {
        waypoints = new Dictionary<int, GameObject>();
    }

    public void SpawnWaypoint()
    {
        int waypointID = UnityEngine.Random.Range(0, 10000000);

        List<byte> byteList = new List<byte>();
        byteList.AddRange(BitConverter.GetBytes(waypointID));
        byteList.Add((byte)'C');

        Vector3 nPos = map.NormalizePosition(bodyOrigin.transform.position);
        byteList.AddRange(BitConverter.GetBytes(nPos.x));
        byteList.AddRange(BitConverter.GetBytes(nPos.y));

        GameObject waypoint = Instantiate(waypointPrefab, map.GetPositionFromNormalized(nPos), Quaternion.identity);
        waypoint.transform.SetParent(map.transform);

        waypoints.Add(waypointID, waypoint);
        MIKEServerManager.Main.SendDataReliably(ServiceType.Waypoint, byteList.ToArray());
    }

    public void SpawnWaypoint(int waypointID, Vector3 pos)
    {
        GameObject waypoint = Instantiate(waypointPrefab, pos, Quaternion.identity);
        waypoint.transform.SetParent(map.transform);

        waypoints.Add(waypointID, waypoint);
    }

    public void MoveWaypoint(int waypointID, Vector3 waypointPos)
    {
        waypoints[waypointID].transform.position = waypointPos;
    }

    public void DeleteWaypoint(int waypointID)
    {
        Destroy(waypoints[waypointID]);
        waypoints.Remove(waypointID);
    }

    // FOR TESTING
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SpawnWaypoint();
        }
    }
}
