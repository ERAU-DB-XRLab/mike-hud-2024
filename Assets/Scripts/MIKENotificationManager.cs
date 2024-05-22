using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class MIKENotificationManager : MonoBehaviour
{

    public static MIKENotificationManager Main { get; private set; }

    [SerializeField] private GameObject notificationPrefab;
    [SerializeField] private Transform notificationParent;

    private Queue<Notification> notifications = new Queue<Notification>();

    void Awake()
    {
        Main = this;
        StartCoroutine(DequeueNotifications());
    }

    public void SendNotification(string header, string content, Color c, float time)
    {
        
        notifications.Enqueue(new Notification() { header = header, content = content, c = c, time = time });

    }

    private void Create(string header, string content, Color c, float time)
    {
        MIKENotification notification = Instantiate(notificationPrefab, notificationParent).GetComponent<MIKENotification>();
        notification.transform.localPosition = Vector3.zero;
        notification.SetText(header, content, c, time);

        MIKESFXManager.main.PlaySFX("Notification", 1f);
    }

    public IEnumerator DequeueNotifications()
    {
        if(notifications.Count > 0)
        {
            Notification n = notifications.Dequeue();
            Create(n.header, n.content, n.c, n.time);
            yield return new WaitForSeconds(n.time * 1.25f);
        } else
        {
            yield return new WaitForSeconds(1);
        }
        
        StartCoroutine(DequeueNotifications());
    }

}

class Notification
{
    public string header;
    public string content;
    public Color c;
    public float time;
}