using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MIKEHeadInteractor : MonoBehaviour
{

    private MIKEWidget currentWidget;
    [SerializeField] private InputActionProperty property;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(new Ray(transform.position, transform.forward), out hit, Mathf.Infinity, MIKEResources.Main.MIKEWidgetLayerMask))
        {

            MIKEWidget widget = hit.transform.GetComponent<MIKEWidget>();

            if (currentWidget && currentWidget != widget)
                currentWidget.OnHoverExit();

            currentWidget = widget;
            currentWidget.OnHoverEnter();

        } else
        {
            if(currentWidget)
            {
                currentWidget.OnHoverExit();
                currentWidget = null;
            }
        }


        if(property.action.WasPressedThisFrame())
        {
            if(currentWidget)
            {
                currentWidget.OnClickStart();
            }
        }

        if(property.action.WasReleasedThisFrame())
        {
            if(currentWidget)
            {
                currentWidget.OnClickEnd();
            }
        }

    }
}
