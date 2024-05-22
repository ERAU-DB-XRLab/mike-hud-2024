using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MIKELMCCHandService : MIKEService
{

    [SerializeField] private Transform leftHand, rightHand;
    [SerializeField] private Animator leftHandAnim, rightHandAnim;

    private float lastPacketTime = -1000;

    void Start()
    {
        MIKEInputManager.Main.RegisterService(ServiceType.Hand, this);    
    }

    void Update()
    {
        if(Time.timeSinceLevelLoad - lastPacketTime > 5)
        {
            leftHand.gameObject.SetActive(false);
            rightHand.gameObject.SetActive(false);
        } else
        {
            leftHand.gameObject.SetActive(true);
            rightHand.gameObject.SetActive(true);
        }
    }

    public override void ReceiveData(MIKEPacket packet)
    {

        lastPacketTime = Time.timeSinceLevelLoad;

        Vector3 leftHandPos = packet.ReadVector3();
        Quaternion leftHandRot = packet.ReadQuaternion();
        bool gripDownLeft = packet.ReadBool();
        bool triggerDownLeft = packet.ReadBool();

        Vector3 rightHandPos = packet.ReadVector3();
        Quaternion rightHandRot = packet.ReadQuaternion();
        bool gripDownRight = packet.ReadBool();
        bool triggerDownRight = packet.ReadBool();

        leftHand.transform.SetLocalPositionAndRotation(Vector3.Lerp(leftHand.localPosition, leftHandPos, 0.5f), Quaternion.Lerp(leftHand.localRotation, leftHandRot, 0.5f));
        leftHandAnim.SetBool("GripDown", gripDownLeft);
        leftHandAnim.SetBool("TriggerDown", triggerDownLeft);

        rightHand.transform.SetLocalPositionAndRotation(Vector3.Lerp(rightHand.localPosition, rightHandPos, 0.5f), Quaternion.Lerp(rightHand.localRotation, rightHandRot, 0.5f));
        rightHandAnim.SetBool("GripDown", gripDownRight);
        rightHandAnim.SetBool("TriggerDown", triggerDownRight);

    }

}
