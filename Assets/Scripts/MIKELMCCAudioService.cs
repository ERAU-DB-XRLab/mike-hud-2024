using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class MIKELMCCAudioService : MIKEService
{

    // Reference data
    [SerializeField] private MIKELMCCWidget widget;

    // Packet data
    private bool parsing = false;
    //private int packetsParsed = 0;
    private List<byte> mainData = new List<byte>();
    private float firstPacketTime;


    void Start()
    {
        Service = ServiceType.Audio;
        IsReliable = false;
        MIKEInputManager.Main.RegisterService(Service, this);
    }

    public override void ReceiveData(MIKEPacket packet)
    {
        if (!parsing)
        {
            parsing = true;
            firstPacketTime = Time.realtimeSinceStartup;
        }

        mainData.AddRange(packet.UnreadByteArray);

        //if (packetsParsed >= packetCount)
        //{
        //    StoreClip(mainData.ToArray());
        //    ResetData();
        //}
    }

    void Update()
    {
        if (parsing)
        {
            if (Time.realtimeSinceStartup - firstPacketTime > 1f)
            {
                // Cutoff
                StoreClip(mainData.ToArray());
                ResetData();
            }
        }
    }

    public void ResetData()
    {
        mainData.Clear();
        //packetsParsed = 0;
        parsing = false;
    }

    public void StoreClip(byte[] data)
    {

        Debug.Log("clip stored!");
        float[] floatArray = new float[Mathf.CeilToInt(data.Length / 4f)];
        Buffer.BlockCopy(data, 0, floatArray, 0, data.Length);

        AudioClip clip = AudioClip.Create("Test", floatArray.Length, 1, 16000, false);
        clip.SetData(floatArray, 0);

        widget.CreateNewMessage(clip);

        MIKENotificationManager.Main.SendNotification("NOTIFICATION", "New LMCC Message", MIKEResources.Main.PositiveNotificationColor, 2.5f);

    }

}
