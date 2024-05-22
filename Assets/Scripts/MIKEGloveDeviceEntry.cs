using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MIKEGloveDeviceEntry : MIKEInputDeviceEntry
{

    private int lastMiddle = 0;
    private int lastRing = 0;
    private int lastPinky;

    bool recording = false;

    new void Start()
    {
        base.Start();
        PlayerManager.Instance.ShowFingers();
        MIKEAudioTransmitter.Main.OnRecordingStart += () => recording = true;
        MIKEAudioTransmitter.Main.OnRecordingStop += () => recording = false;
    }

    public override void ReceiveData(byte[] data)
    {

        base.ReceiveData(data);
        int index = data[1];
        int middle = data[2];
        int ring = data[3];
        int pinky = data[4];

        if(middle == 1 && lastMiddle == 0)
        {
            MIKEHeadInteractor.Main.Click();
        } else
        if(middle == 0 && lastMiddle == 1)
        {
            MIKEHeadInteractor.Main.Unclick();
        }

        if(ring == 1 && lastRing == 0)
        {
            MIKEScreenManager.Main.gameObject.SetActive(!MIKEScreenManager.Main.gameObject.activeSelf);
        }

        if(pinky == 1 && lastPinky == 0)
        {
            if(!recording)
            {
                MIKEAudioTransmitter.Main.StartRecording();
            } else
            {
                MIKEAudioTransmitter.Main.StopRecording();
            }
        }

        lastMiddle = middle;
        lastRing = ring;
        lastPinky = pinky;

    }

}
