using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MIKEAudioReceiver : MonoBehaviour
{

    public static MIKEAudioReceiver Main;

    public AudioSource source;

    int packetCount = -1;
    int packetsParsed = 0;
    List<byte> mainData = new List<byte>();

    private float firstPacketTime;

    // Start is called before the first frame update
    void Start()
    {
        Main = this;       
    }

    void Update()
    {
        if(packetCount != -1)
        {
            if(Time.realtimeSinceStartup - firstPacketTime > 1.5f)
            {
                // Cutoff
                PlayClip(mainData.ToArray());
                ResetData();
                Debug.Log("CUTOFF");
            }
        }
    }

    public void Receive(byte[] data)
    {

        List<byte> dataAsList = data.ToList();

        if (packetCount == -1)
        {
            packetCount = data[0];
            // remove packet byte
            dataAsList.RemoveAt(0);
            firstPacketTime = Time.realtimeSinceStartup;
        }

        mainData.AddRange(dataAsList);
        packetsParsed++;
        Debug.Log(packetsParsed);

        if(packetsParsed >= packetCount)
        {
            PlayClip(mainData.ToArray());
            ResetData();
        }

    }

    public void ResetData()
    {
        mainData.Clear();
        packetsParsed = 0;
        packetCount = -1;
    }

    public void PlayClip(byte[] data)
    {

        float[] floatArray = new float[Mathf.CeilToInt(data.Length / 4f)];
        Buffer.BlockCopy(data, 0, floatArray, 0, data.Length);

        AudioClip clip = AudioClip.Create("Test", floatArray.Length, 1, 16000, false);
        clip.SetData(floatArray, 0);

        source.clip = clip;
        source.Play();

        Debug.Log("Playing: " + floatArray.Length);

    }

}
