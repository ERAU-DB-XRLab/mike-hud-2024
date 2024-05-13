using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MIKEIntro : MonoBehaviour
{
    public bool Complete { get; private set; } = false;

    [SerializeField] private GameObject main;
    private Dissipate[] dissipate;

    public void Play()
    {
        main.SetActive(false);
        dissipate = GetComponentsInChildren<Dissipate>();
        foreach (Dissipate d in dissipate)
        {
            d.DissipateStart(false, 3f);
        }
        StartCoroutine(ShowApp());
    }

    public IEnumerator ShowApp()
    {
        yield return new WaitForSeconds(3f);
        main.SetActive(true);
        Complete = true;
    }

}
