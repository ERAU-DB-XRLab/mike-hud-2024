using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class MIKENavigationManager : MonoBehaviour
{

    [SerializeField] private MIKEMap map;
    [SerializeField] private NavMeshAgent player;
    [SerializeField] private LineRenderer r;

    private bool pathActive;

    public void SetDesiredPosition(Vector2 normalizedPos)
    {
        player.destination = map.GetPositionFromNormalized(normalizedPos);
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
