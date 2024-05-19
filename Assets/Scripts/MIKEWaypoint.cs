using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MIKEWaypoint : MonoBehaviour
{
    public int WaypointID { get; set; }
    public string WaypointNumber { get; set; }
    public MIKEWidgetValue WaypointText { get => waypointText; }

    [SerializeField] private MIKEWidgetValue waypointText;
}
