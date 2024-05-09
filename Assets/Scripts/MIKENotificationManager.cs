using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MIKENotificationManager : MonoBehaviour
{

    public static MIKENotificationManager Main { get; private set; }

    [SerializeField] private GameObject notificationPrefab;
    [SerializeField] private Transform notificationParent;

    void Awake()
    {
        Main = this;    
    }

    public void SendNotification(string header, string content, Color c, float time)
    {
        
        MIKENotification notification = Instantiate(notificationPrefab, notificationParent).GetComponent<MIKENotification>();
        notification.transform.localPosition = Vector3.zero;
        notification.SetText(header, content, c, time);

        MIKESFXManager.main.PlaySFX("Notification", 1f);

    }
}
