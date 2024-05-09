using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MIKELMCCWidget : MIKEWidget
{

    [SerializeField] private Transform lmccMessageParent;
    [SerializeField] private GameObject lmccMessagePrefab;

    new void Awake()
    {
        base.Awake();
    }

    public void CreateNewMessage(AudioClip clip)
    {
        MIKELMCCMessage newMessage = Instantiate(lmccMessagePrefab, lmccMessageParent).GetComponent<MIKELMCCMessage>();
        newMessage.LoadClip(clip);
    }

}
