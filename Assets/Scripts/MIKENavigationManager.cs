using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.TextCore.Text;

public class MIKENavigationManager : MonoBehaviour
{

    public static MIKENavigationManager Main { get; private set; }

    [SerializeField] private MIKEMap map;
    [SerializeField] private Transform player;
    [SerializeField] private LineRenderer r;

    private NavMeshPath path;
    private Vector3 endPos;
    private bool pathActive;

    void Awake()
    {
        Main = this;
        StartCoroutine(UpdatePath());
    }

    public void SetDesiredPosition(Vector3 pos)
    {
        pathActive = true;
        endPos = pos;
        path = new NavMeshPath();
    }

    public IEnumerator UpdatePath()
    {
        if(pathActive)
        {
            NavMeshHit hit;
            if(NavMesh.SamplePosition(endPos, out hit, 100, -1))
            {
                NavMesh.CalculatePath(player.position, hit.position, NavMesh.AllAreas, path);
                Vector3[] corners = path.corners;
                r.positionCount = corners.Length;
                r.SetPositions(corners);
            }
            
        }

        yield return new WaitForSeconds(3);
        StartCoroutine(UpdatePath());

    }

}
