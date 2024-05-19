using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MIKESpecDataViewer : MonoBehaviour
{
    [SerializeField] private TMP_Text specName;
    [SerializeField] private TMP_Text specID;
    [SerializeField] private TMP_Text SiO2;
    [SerializeField] private TMP_Text TiO2;
    [SerializeField] private TMP_Text Al203;
    [SerializeField] private TMP_Text FeO;
    [SerializeField] private TMP_Text MnO;
    [SerializeField] private TMP_Text MgO;
    [SerializeField] private TMP_Text CaO;
    [SerializeField] private TMP_Text K2O;
    [SerializeField] private TMP_Text P203;
    [SerializeField] private TMP_Text other;

    public void View(SpecData data)
    {
        specName.text = data.name;
        specID.text = data.id.ToString();
        SiO2.text = data.data.SiO2.ToString();
        TiO2.text = data.data.TiO2.ToString();
        Al203.text = data.data.Al2O3.ToString();
        FeO.text = data.data.FeO.ToString();
        MnO.text = data.data.MnO.ToString();
        MgO.text = data.data.MgO.ToString();
        CaO.text = data.data.CaO.ToString();
        K2O.text = data.data.K2O.ToString();
        P203.text = data.data.P2O3.ToString();
        other.text = data.data.other.ToString();
    }

}
