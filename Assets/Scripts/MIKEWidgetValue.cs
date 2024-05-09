using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class MIKEWidgetValue : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private string units;

    public void SetValue(float value)
    {
        text.SetText(value.ToString("0.00") + "    [" + units + "]");
    }

    public void SetValue(bool value)
    {
        text.SetText(value ? "<color=green>" : "<color=red>" + value.ToString());
    }

}
