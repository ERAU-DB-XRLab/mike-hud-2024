using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class MIKEButton : MIKEWidget, ISelectHandler, IDeselectHandler
{

    public UnityEvent Clicked;

    protected Image highlight;
    protected BoxCollider col;

    protected new void Awake()
    {
        base.Awake();
        col = GetComponent<BoxCollider>();
        highlight = GetComponent<Image>();
        highlight.enabled = false;
    }

    public void OnSelect(BaseEventData e)
    {
        highlight.enabled = true;
    }

    public void OnDeselect(BaseEventData e)
    {
        highlight.enabled = false;
    }

    public override void OnHoverEnter()
    {
        base.OnHoverEnter();
        highlight.enabled = true;
    }

    public override void OnHoverExit()
    {
        base.OnHoverExit();
        highlight.enabled = false;
    }

    public override void OnClickStart()
    {
        base.OnClickStart();
        highlight.color = Color.green;
        MIKESFXManager.main.PlaySFX("ButtonClick", 0.5f);
        Clicked.Invoke();
    }

    public override void OnClickEnd()
    {
        base.OnClickEnd();
        highlight.color = Color.white;
    }


}
