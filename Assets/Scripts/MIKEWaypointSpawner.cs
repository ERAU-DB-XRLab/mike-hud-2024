using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MIKEWaypointSpawner : MonoBehaviour
{

    public static MIKEWaypointSpawner Main;

    [SerializeField] private GameObject waypointPrefab;
    [SerializeField] private MIKEMap map;

    // Start is called before the first frame update
    void Awake()
    {
        Main = this;
    }

    public void SpawnWaypoint(Vector3 pos)
    {
        GameObject waypoint = Instantiate(waypointPrefab, pos, Quaternion.identity);
        waypoint.transform.SetParent(map.transform);
    }

}
