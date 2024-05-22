using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MIKETutorialManager : MonoBehaviour
{

    public static MIKETutorialManager Main { get; private set; }

    [SerializeField] private GameObject tutorialPrompt, leftTooltips, rightTooltips;

    void Awake()
    {
        Main = this;
    }

    public void ShowPrompt()
    {
        tutorialPrompt.SetActive(true);
    }

    public void RespondToPrompt(bool b)
    {

        if (b)
        {
            StartCoroutine(Tutorial());
        }

        tutorialPrompt.SetActive(false);

    }

    public IEnumerator Tutorial()
    {

        MIKESFXManager.main.PlaySFX("Tutorial/ShowAround", 1f);
        yield return new WaitForSeconds(5);
        MIKESFXManager.main.PlaySFX("Tutorial/OnYourLeft", 1f);
        leftTooltips.SetActive(true);
        yield return new WaitForSeconds(8);
        MIKESFXManager.main.PlaySFX("Tutorial/FeelFree", 1f);
        yield return new WaitForSeconds(12);
        leftTooltips.SetActive(false);
        MIKESFXManager.main.PlaySFX("Tutorial/OnYourRight", 1f);
        rightTooltips.SetActive(true);
        yield return new WaitForSeconds(8f);
        MIKESFXManager.main.PlaySFX("Tutorial/FeelFree", 1f);
        yield return new WaitForSeconds(12);
        rightTooltips.SetActive(false);
        MIKENotificationManager.Main.SendNotification("TUTORIAL", "Tutorial Complete!", Color.green, 2f);

    }

}
