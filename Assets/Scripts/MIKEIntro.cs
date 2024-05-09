using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MIKEIntro : MonoBehaviour
{

    [SerializeField] private GameObject main;
    private Dissipate[] dissipate;

    // Start is called before the first frame update
    void Start()
    {
        main.SetActive(false);
        dissipate = GetComponentsInChildren<Dissipate>();
        foreach(Dissipate d in dissipate)
        {
            d.DissipateStart(false, 3f);
        }
        StartCoroutine(ShowApp());
    }

    public IEnumerator ShowApp()
    {
        yield return new WaitForSeconds(3f);
        main.SetActive(true);
    }

}
