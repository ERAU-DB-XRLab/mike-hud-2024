using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class MIKEInputDeviceEntry : MIKEButton
{

    public UnityEvent<int> Disconnected;

    protected bool expanded;
    protected int deviceID;
    protected string deviceType;
    protected float lastCommunicationTime;

    private bool connected = true;


    protected new void Awake()
    {
        base.Awake();
    }

    protected void Start()
    {
        Clicked.AddListener(ButtonClicked);
    }

    protected void Update()
    {
        if (Time.realtimeSinceStartup - lastCommunicationTime > 5f && connected)
        {
            Disconnected.Invoke(deviceID);
            connected = false;
        }
    }

    public void ButtonClicked()
    {
        SetExpanded(!expanded);
        if (expanded)
        {
            MIKEInputManager.Main.CloseAllEntries(this);
        }
    }

    public void SetExpanded(bool b)
    {

        expanded = b;

        if (b)
        {
            StopCoroutine(Contract());
            StartCoroutine(Expand());
            highlight.enabled = true;
        }
        else
        {
            StopCoroutine(Expand());
            StartCoroutine(Contract());
        }

    }

    public virtual IEnumerator Expand()
    {
        yield return new WaitForSeconds(1);
    }

    public virtual IEnumerator Contract()
    {
        yield return new WaitForSeconds(1);
    }


    public virtual void Init(int deviceID, string deviceType)
    {
        this.deviceID = deviceID;
        this.deviceType = deviceType;
    }

    public override void OnHoverExit()
    {
        base.OnHoverExit();
        if (expanded)
            highlight.enabled = true;
    }

    public bool IsExpanded()
    {
        return expanded;
    }

    public virtual void ReceiveData(byte[] data)
    {
        lastCommunicationTime = Time.realtimeSinceStartup;
    }

}
