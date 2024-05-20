using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MIKEGloveDeviceEntry : MIKEInputDeviceEntry
{

    private int indexLast = 0;

    public override void ReceiveData(byte[] data)
    {

        base.ReceiveData(data);
        int index = data[1];
        int middle = data[2];
        int ring = data[3];
        int pinky = data[4];

        if(ring == 1 && indexLast == 0)
        {
            MIKEHeadInteractor.Main.Click();
        } else
        if(ring == 0 && indexLast == 1)
        {
            MIKEHeadInteractor.Main.Unclick();
        }

        indexLast = ring;

    }

}
