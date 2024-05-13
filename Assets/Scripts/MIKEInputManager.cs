using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MIKEInputManager : MonoBehaviour
{

    public static MIKEInputManager Main { get; private set; }

    // DEVICE REGISTRATION

    [SerializeField] private Transform devicesParent;

    private readonly Dictionary<int, string> deviceType = new Dictionary<int, string>();

    // Input devices
    private Dictionary<int, MIKEInputDeviceEntry> inputDeviceEntries = new Dictionary<int, MIKEInputDeviceEntry>();

    // SERVICES
    private Dictionary<int, MIKEService> services = new Dictionary<int, MIKEService>();

    private int otherReliableCounter = 0;
    private int reliablePacketCount;

    void Awake()
    {
        if (Main == null)
            Main = this;
        else
            Destroy(this);

        GenerateDevices();
    }

    private void GenerateDevices()
    {
        deviceType.Add(0, "Console");
        deviceType.Add(2, "Camera");
        deviceType.Add(4, "LMCC");
        deviceType.Add(5, "HUD");
    }

    public void RegisterInputDevice(int id)
    {

        string type = deviceType[id];

        // Create new entry
        MIKEInputDeviceEntry entry = Instantiate(Resources.Load<GameObject>(type + "Entry"), devicesParent).GetComponent<MIKEInputDeviceEntry>();
        entry.Init(id, type);
        entry.Disconnected.AddListener(DeviceDisconnected);

        // Populate dictionary
        inputDeviceEntries.Add(id, entry);

        // Notify user
        MIKENotificationManager.Main.SendNotification("NOTIFICATION", "New Input Device Connected!", MIKEResources.Main.PositiveNotificationColor, 2.5f);

    }

    public void RegisterService(ServiceType type, MIKEService service)
    {
        services.Add((int)type, service);
    }

    public void ReceiveInput(byte[] data)
    {
        if (data == null || data.Length == 0)
        {
            Debug.Log("Empty data received");
            return;
        }

        int id = data[0];

        // First check if it's a service
        if (services.ContainsKey(id))
        {
            var packet = new MIKEPacket(data);
            packet.CurrentIndex++;
            if (services[id].IsReliable)
            {
                int rc = packet.ReadInt();
                if (rc > otherReliableCounter)
                {
                    reliablePacketCount = 1;
                    otherReliableCounter = rc;
                    services[id].ReceiveData(packet);
                }
                else if (rc == otherReliableCounter)
                {
                    reliablePacketCount++;
                }
                else
                {
                    Debug.LogWarning("Received an old reliable packet with rc: " + rc);
                }

                if (reliablePacketCount == MIKEServerManager.Main.ReliableSendCount)
                {
                    Debug.Log("No reliable packets lost!");
                }
            }
            else
            {
                services[id].ReceiveData(packet);
            }
            return;
        }

        // If not a service, then handle it as an input device
        if (!inputDeviceEntries.ContainsKey(id) && deviceType.ContainsKey(id))
        {
            RegisterInputDevice(id);
        }

        if (inputDeviceEntries.ContainsKey(id))
            inputDeviceEntries[id].ReceiveData(data);

    }

    public void DeviceDisconnected(int id)
    {
        MIKENotificationManager.Main.SendNotification("NOTIFICATION", "Input Device Disconnected", MIKEResources.Main.NegativeNotificationColor, 2.5f);
        Destroy(inputDeviceEntries[id].gameObject);
        inputDeviceEntries.Remove(id);
    }

    public void CloseAllEntries(MIKEInputDeviceEntry exception)
    {
        foreach (MIKEInputDeviceEntry e in inputDeviceEntries.Values)
        {
            if (e != exception && e.IsExpanded())
                e.SetExpanded(false);
        }

        exception.transform.SetAsFirstSibling();
    }
}
