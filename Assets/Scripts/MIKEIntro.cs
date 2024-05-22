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
        MIKESFXManager.main.PlaySFX("Welcome", 1f);
        dissipate = GetComponentsInChildren<Dissipate>();
        foreach (Dissipate d in dissipate)
        {
            d.DissipateStart(false, 3f);
        }
        StartCoroutine(ShowApp());
    }

    public IEnumerator ShowApp()
    {
        yield return new WaitForSeconds(6f);
        main.SetActive(true);
        MIKETutorialManager.Main.ShowPrompt();
        Complete = true;
    }

}
