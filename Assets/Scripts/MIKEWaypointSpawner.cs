using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MIKEWaypointSpawner : MonoBehaviour
{
    public static MIKEWaypointSpawner Main { get; private set; }

    [SerializeField] private GameObject waypointPrefab;
    [SerializeField] private GameObject lmccWaypointPrefab;
    [SerializeField] private MIKEMap map;
    [SerializeField] private GameObject bodyOrigin;

    public int HUDWaypointCount { get; private set; } = 0;
    public Dictionary<int, MIKEWaypoint> Waypoints { get => waypoints; }
    private Dictionary<int, MIKEWaypoint> waypoints;

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
        waypoints = new Dictionary<int, MIKEWaypoint>();
    }

    public void SpawnWaypoint()
    {

        int waypointID = UnityEngine.Random.Range(0, 10000000);
        Vector2 normalizedPos = map.NormalizePosition(map.transform.InverseTransformPoint(bodyOrigin.transform.position));
        GameObject waypoint = Instantiate(waypointPrefab, map.GetPositionFromNormalized(normalizedPos), Quaternion.identity);
        waypoint.transform.SetParent(map.transform);
        waypoints.Add(waypointID, waypoint.GetComponent<MIKEWaypoint>());
        AngleWaypoint(waypoint);


        // SEND TO LMCC
        var packet = new MIKEPacket();
        packet.Write((int)WaypointServiceType.Create);
        packet.Write(waypointID);
        packet.Write(normalizedPos.x);
        packet.Write(normalizedPos.y);
        packet.Write(++HUDWaypointCount);

        waypoints[waypointID].WaypointID = waypointID;
        waypoints[waypointID].WaypointNumber = "HUD " + HUDWaypointCount.ToString();
        waypoints[waypointID].WaypointText.SetValue(HUDWaypointCount.ToString());

        MIKEServerManager.Main.SendData(ServiceType.Waypoint, packet, DeliveryType.Reliable);
    }

    public void SpawnLMCCWaypoint(int waypointID, int waypointNum, Vector3 pos)
    {
        GameObject waypoint = Instantiate(lmccWaypointPrefab, pos, Quaternion.identity);
        waypoint.transform.SetParent(map.transform);

        waypoints.Add(waypointID, waypoint.GetComponent<MIKEWaypoint>());
        AngleWaypoint(waypoint);

        Debug.Log("WAYPOINT");
        MIKENotificationManager.Main.SendNotification("LMCC", "Waypoint spawned!", Color.green, 2f);
        MIKENavigationManager.Main.SetDesiredPosition(waypoint.transform.position);

        waypoints[waypointID].WaypointID = waypointID;
        waypoints[waypointID].WaypointNumber = "LMCC " + waypointNum.ToString();
        waypoints[waypointID].WaypointText.SetValue(waypointNum.ToString());
    }

    public void MoveLMCCWaypoint(int waypointID, Vector3 waypointPos)
    {
        waypoints[waypointID].transform.position = waypointPos;
        AngleWaypoint(waypoints[waypointID].gameObject);
        MIKENavigationManager.Main.SetDesiredPosition(waypointPos);
    }

    public void DeleteWaypoint(int waypointID)
    {
        Destroy(waypoints[waypointID].gameObject);
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
