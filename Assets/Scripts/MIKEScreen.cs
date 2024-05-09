using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MIKEScreen : MonoBehaviour
{
    [SerializeField] private MIKEWidget[] widgets;
    [SerializeField] private TextMeshProUGUI title;
    [SerializeField] private int defaultWidgetIndex;

    void Start()
    {
        ActivateWidget(widgets[defaultWidgetIndex]);
    }

    public void ActivateWidget(MIKEWidget widget)
    {

        foreach(MIKEWidget mikeWidget in widgets)
        {
            if(mikeWidget == widget)
            {
                mikeWidget.Activate();
            } else
            {
                if(mikeWidget.gameObject.activeSelf)
                {
                    mikeWidget.Deactivate();
                }
            }
        }

        title.SetText(widget.GetWidgetName());

    }

}
