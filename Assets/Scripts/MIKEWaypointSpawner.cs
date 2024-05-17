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

    [Header("DEBUG")]
    [SerializeField] private bool debugMode = false;
    [SerializeField] private KeyCode spawnKey = KeyCode.Space;

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
        Vector2 normalizedPos = map.NormalizePosition(map.transform.InverseTransformPoint(bodyOrigin.transform.position));
        GameObject waypoint = Instantiate(waypointPrefab, map.GetPositionFromNormalized(normalizedPos), Quaternion.identity);
        waypoint.transform.SetParent(map.transform);
        waypoints.Add(waypointID, waypoint);
        AngleWaypoint(waypoint);

        // SEND TO LMCC
        var packet = new MIKEPacket();
        packet.Write((int)WaypointServiceType.Create);
        packet.Write(waypointID);
        packet.Write(normalizedPos.x);
        packet.Write(normalizedPos.y);
        MIKEServerManager.Main.SendData(ServiceType.Waypoint, packet, DeliveryType.Reliable);
    }

    public void SpawnWaypoint(int waypointID, Vector3 pos)
    {
        GameObject waypoint = Instantiate(waypointPrefab, pos, Quaternion.identity);
        waypoint.transform.SetParent(map.transform);

        waypoints.Add(waypointID, waypoint);
        AngleWaypoint(waypoint);
    }

    public void MoveWaypoint(int waypointID, Vector3 waypointPos)
    {
        waypoints[waypointID].transform.position = waypointPos;
        AngleWaypoint(waypoints[waypointID]);
    }

    public void DeleteWaypoint(int waypointID)
    {
        Destroy(waypoints[waypointID]);
        waypoints.Remove(waypointID);
    }

    private void AngleWaypoint(GameObject waypoint)
    {
        if (map.IsPositionOnMap(waypoint.transform.position, out RaycastHit hit))
        {
            waypoint.transform.GetChild(1).transform.rotation = Quaternion.LookRotation(hit.normal);
        }
    }

    // FOR TESTING
    void Update()
    {
        if (debugMode)
        {
            if (Input.GetKeyDown(spawnKey))
            {
                SpawnWaypoint();
            }
        }
    }
}
