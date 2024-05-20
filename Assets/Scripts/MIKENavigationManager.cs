using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class MIKENavigationManager : MonoBehaviour
{

    public static MIKENavigationManager Main { get; private set; }

    [SerializeField] private MIKEMap map;
    [SerializeField] private NavMeshAgent player;
    [SerializeField] private LineRenderer r;

    private bool pathActive;

    void Awake()
    {
        Main = this;    
        StartCoroutine(UpdatePath());
    }

    public void SetDesiredPosition(Vector3 pos)
    {
        Debug.Log("Setting desired pos BRUH");
        player.destination = pos;
        pathActive = true;
    }

    public IEnumerator UpdatePath()
    {
        if(pathActive)
        {
            Vector3[] corners = player.path.corners;
            for(int i = 0; i < corners.Length; i++)
            {
                corners[i] = map.transform.InverseTransformPoint(corners[i]);
            }
            r.positionCount = corners.Length;
            r.SetPositions(corners);
        }

        yield return new WaitForSeconds(3);
        StartCoroutine(UpdatePath());

    }

}
