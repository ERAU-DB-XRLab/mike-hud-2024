using System;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class MIKEAudioTransmitter : MonoBehaviour
{

    private AudioClip currentClip;
    private float[] audioData;
    private float timer;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            currentClip = Microphone.Start(Microphone.devices[0], false, 30, 16000);
            timer = 0;
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            Microphone.End(Microphone.devices[0]);
            audioData = new float[currentClip.samples * currentClip.channels];
            currentClip.GetData(audioData, 0);

            List<float> editedData = audioData.ToList().GetRange(0, (int)(16000f * timer));
            audioData = editedData.ToArray();

            SendData();
        }

        timer += Time.deltaTime;

    }

    public void SendData()
    {
        byte[] byteArray = new byte[audioData.Length * 4];
        Buffer.BlockCopy(audioData, 0, byteArray, 0, byteArray.Length);
        Debug.Log("Sending sample count: " + audioData.Length);
        var packet = new MIKEPacket(byteArray);
        MIKEServerManager.Main.SendData(ServiceType.Audio, packet, DeliveryType.Unreliable);
    }
}
