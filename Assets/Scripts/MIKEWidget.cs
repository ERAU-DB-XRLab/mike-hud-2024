using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MIKEWidget : MonoBehaviour
{

    [SerializeField] protected string widgetName;

    protected void Awake()
    {
        gameObject.layer = MIKEResources.Main.MIKEWidgetLayer;
    }

    public virtual void Activate()
    {
        gameObject.SetActive(true);
    }

    public virtual void Deactivate()
    {
        gameObject.SetActive(false);
    }

    public virtual void OnHoverEnter()
    {
        //
    }

    public virtual void OnHoverExit()
    {
        //
    }

    public virtual void OnClickStart()
    {
        //
    }

    public virtual void OnClickEnd()
    {
        //
    }

    public string GetWidgetName()
    {
        return widgetName;
    }

}
