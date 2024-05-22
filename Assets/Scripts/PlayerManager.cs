using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.MagicLeap;
using static UnityEngine.XR.MagicLeap.InputSubsystem.Extensions;

public class PlayerManager : MonoBehaviour
{

    public static PlayerManager Instance { get; private set; }

    private UnityEngine.XR.InputDevice leftHandDevice, rightHandDevice;

    

    [SerializeField] private Transform leftHand, rightHand, allFingers, index, middle, ring, pinky;
    [SerializeField] private UnityEngine.InputSystem.InputActionProperty lHand, rHand;

    private float timer = 15;

    private void Start()
    {
        Instance = this;
        InputSubsystem.Extensions.MLHandTracking.StartTracking();
        MLGestureClassification.StartTracking();
    }

    void Update()
    {

        leftHand.transform.localPosition = lHand.action.ReadValue<Vector3>();
        rightHand.transform.localPosition = rHand.action.ReadValue<Vector3>();

        if (!leftHandDevice.isValid || !rightHandDevice.isValid)
        {
            leftHandDevice = InputSubsystem.Utils.FindMagicLeapDevice(InputDeviceCharacteristics.HandTracking | InputDeviceCharacteristics.Left);
            rightHandDevice = InputSubsystem.Utils.FindMagicLeapDevice(InputDeviceCharacteristics.HandTracking | InputDeviceCharacteristics.Right);
            return;
        }

        List<Bone> bones = new List<Bone>();
        leftHandDevice.TryGetFeatureValue(CommonUsages.handData, out Hand hand);

        if(timer < 15)
        {

            hand.TryGetFingerBones(HandFinger.Index, bones);
            bones[0].TryGetPosition(out Vector3 indexPos);
            index.localPosition = indexPos;

            hand.TryGetFingerBones(HandFinger.Middle, bones);
            bones[0].TryGetPosition(out Vector3 middlePos);
            middle.localPosition = middlePos;

            hand.TryGetFingerBones(HandFinger.Ring, bones);
            bones[0].TryGetPosition(out Vector3 ringPos);
            ring.localPosition = ringPos;

            hand.TryGetFingerBones(HandFinger.Pinky, bones);
            bones[0].TryGetPosition(out Vector3 pinkyPos);
            pinky.localPosition = pinkyPos;

        } else
        {
            if(allFingers.gameObject.activeSelf)
            {
                allFingers.gameObject.SetActive(false);
            }
        }

        timer += Time.deltaTime;

    }

    public void ShowFingers()
    {
        timer = 0;
        allFingers.gameObject.SetActive(true);
    }

}
