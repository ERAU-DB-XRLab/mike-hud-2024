using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.MagicLeap;

public class PlayerManager : MonoBehaviour
{
    private InputDevice leftHandDevice;
    private InputDevice rightHandDevice;
    [SerializeField] private Transform leftHand, rightHand;

    void Update()
    {
        if (!leftHandDevice.isValid || !rightHandDevice.isValid)
        {
            leftHandDevice = InputSubsystem.Utils.FindMagicLeapDevice(InputDeviceCharacteristics.HandTracking | InputDeviceCharacteristics.Left);
            rightHandDevice = InputSubsystem.Utils.FindMagicLeapDevice(InputDeviceCharacteristics.HandTracking | InputDeviceCharacteristics.Right);
            return;
        } else
        {
            Debug.Log("VALID!");
        }


        leftHandDevice.TryGetFeatureValue(InputSubsystem.Extensions.DeviceFeatureUsages.Hand.WristCenter, out Bone wristBoneLeft);
        rightHandDevice.TryGetFeatureValue(InputSubsystem.Extensions.DeviceFeatureUsages.Hand.WristCenter, out Bone wristBoneRight);
        wristBoneLeft.TryGetPosition(out Vector3 leftPos);
        wristBoneRight.TryGetPosition(out Vector3 rightPos);

        leftHand.position = leftPos;
        rightHand.position = rightPos;


    }

}
