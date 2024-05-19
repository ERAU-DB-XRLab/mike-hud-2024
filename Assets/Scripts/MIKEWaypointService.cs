using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
public enum WaypointServiceType
{
    Create = 0,
    Move = 1,
    Delete = 2
}

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

    public override void ReceiveData(MIKEPacket packet)
    {
        // Parse waypoint action
        int serviceType = packet.ReadInt();

        // Parse waypoint ID
        int waypointID = packet.ReadInt();

        if (serviceType == (int)WaypointServiceType.Delete)
        {
            MIKEWaypointSpawner.Main.DeleteWaypoint(waypointID);
        }
        else
        {
            // Parse data
            float xPos = packet.ReadFloat();
            float yPos = packet.ReadFloat();
            Vector3 waypointPos = mainMap.GetPositionFromNormalized(new Vector2(xPos, yPos));

            int waypointNum = packet.ReadInt();

            switch (serviceType)
            {
                case (int)WaypointServiceType.Create:
                    MIKEWaypointSpawner.Main.SpawnLMCCWaypoint(waypointID, waypointNum, waypointPos);
                    break;
                case (int)WaypointServiceType.Move:
                    MIKEWaypointSpawner.Main.MoveLMCCWaypoint(waypointID, waypointPos);
                    break;
                default:
                    Debug.LogError("MIKEWaypointService: Invalid waypoint service type");
                    break;
            }
        }
    }
}
