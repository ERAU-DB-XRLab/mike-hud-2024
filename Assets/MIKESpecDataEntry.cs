using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MIKESpecDataEntry : MIKEButton
{

    [SerializeField] private TMP_Text entryName;

    public SpecData SpecData { get; private set; }

    public void SetSpecData(SpecData data)
    {
        this.SpecData = data;
        entryName.SetText("Spec Entry - " + data.id + " - " + DateTime.Now.ToShortTimeString());
    }
}
