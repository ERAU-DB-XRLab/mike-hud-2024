using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MIKELMCCMessage : MIKEButton
{

    [SerializeField] private AudioSource source;
    [SerializeField] private Image fill;
    [SerializeField] private TextMeshProUGUI text;

    void Start()
    {
        Clicked.AddListener(PlayClip);
    }

    public void LoadClip(AudioClip clip)
    {
        source.clip = clip;
        text.SetText("LMCC Message - " + DateTime.Now.ToLongTimeString());
    }

    public void PlayClip()
    {
        source.Play();
    }

    void Update()
    {
        if(source.isPlaying)
        {
            fill.fillAmount = source.time / source.clip.length;
        } else
        {
            fill.fillAmount = 1;
        }
    }

}
