using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MIKEWaypointService : MIKEService
{

    [SerializeField] private MIKEMap mainMap;

    // Start is called before the first frame update
    void Start()
    {
        Service = ServiceType.Waypoint;
        IsReliable = true;
        MIKEInputManager.Main.RegisterService(Service, this);
    }

    public override void ReceiveData(byte[] data)
    {
        Debug.Log("Received Waypoint Data");
        List<byte> dataAsList = data.ToList();

        // remove device ID byte and reliability byte
        dataAsList.RemoveRange(0, 2);

        // Parse waypoint ID
        int waypointID = BitConverter.ToInt32(dataAsList.GetRange(0, 4).ToArray(), 0);
        dataAsList.RemoveRange(0, 4);

        // Parse waypoint action
        char waypointAction = BitConverter.ToChar(dataAsList.GetRange(0, sizeof(char)).ToArray(), 0);
        dataAsList.RemoveRange(0, sizeof(char));

        if (waypointAction == 'D')
        {
            MIKEWaypointSpawner.Main.DeleteWaypoint(waypointID);
            return;
        }

        // Parse data
        float xPos = BitConverter.ToSingle(dataAsList.GetRange(0, 4).ToArray(), 0);
        float yPos = BitConverter.ToSingle(dataAsList.GetRange(4, 4).ToArray(), 0);
        Vector3 waypointPos = mainMap.GetPositionFromNormalized(new Vector2(xPos, yPos));

        switch (waypointAction)
        {
            case 'C':
                MIKEWaypointSpawner.Main.SpawnWaypoint(waypointID, waypointPos);
                break;
            case 'M':
                MIKEWaypointSpawner.Main.MoveWaypoint(waypointID, waypointPos);
                break;
            default:
                Debug.LogWarning("Invalid Waypoint Action");
                break;
        }
        MIKEWaypointSpawner.Main.SpawnWaypoint(waypointID, waypointPos);

    }
}
