using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MIKEHeadInteractor : MonoBehaviour
{

    public static MIKEHeadInteractor Main;

    private MIKEWidget currentWidget;
    [SerializeField] private InputActionProperty property;

    private float lastClickTime = 0;

    // Start is called before the first frame update
    void Awake()
    {
        Main = this;
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
            Click();
            if(Time.timeSinceLevelLoad - lastClickTime < 0.25f)
            {
                MIKEScreenManager.Main.gameObject.SetActive(!MIKEScreenManager.Main.gameObject.activeSelf);
            }
            lastClickTime = Time.timeSinceLevelLoad;
        }

        if(property.action.WasReleasedThisFrame())
        {
            Unclick();
        }

    }

    public void Click()
    {
        if(currentWidget)
        {
            currentWidget.OnClickStart();
        }
    }

    public void Unclick()
    {
        if (currentWidget)
        {
            currentWidget.OnClickEnd();
        }
    }

}
