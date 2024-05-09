using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dissipate : MonoBehaviour
{

    protected MeshRenderer[] mRenderers;

    protected virtual void Awake()
    {
        mRenderers = GetComponentsInChildren<MeshRenderer>();
    }

    public void DissipateDestroy()
    {
        DissipateStart(false, 0f);
        Destroy(this.gameObject, 0.55f);
    }

    public virtual void DissipateStart(bool appear, float delay = 0)
    {
        foreach (MeshRenderer r in mRenderers)
        {
            if (r.material.HasFloat("_DissipationAmount"))
            {
                StartCoroutine(DissipateCoroutine(r.material, appear, delay));
            }
        }
    }

    protected IEnumerator DissipateCoroutine(Material m, bool appear, float delay)
    {

        if (delay > 0)
            yield return new WaitForSeconds(delay);

        for (int i = 0; i < 50; i++)
        {
            m.SetFloat("_DissipationAmount", appear ? (1 - ((float)i / 50f)) : (float)i / 50f);
            yield return new WaitForSeconds(MIKEResources.Main.DissipateTime / 50f);
        }

    }

}
