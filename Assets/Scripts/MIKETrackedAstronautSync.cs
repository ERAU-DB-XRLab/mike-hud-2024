using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MIKETrackedAstronautSync : MonoBehaviour
{
    [SerializeField] private MIKEMap map;
    [SerializeField] private Transform mapParent;
    [Space]
    [SerializeField] private Transform astroHead;
    [SerializeField] private Transform astroLeftHand;
    [SerializeField] private Transform astroRightHand;
    [SerializeField] private float sendInterval = 0.1f;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SendYourTransformData());
    }

    // Update is called once per frame
    void Update()
    {

    }

    private IEnumerator SendYourTransformData()
    {
        while (true)
        {
            var packet = new MIKEPacket();
            packet.Write(map.transform.InverseTransformPoint(astroHead.position));
            packet.Write(Quaternion.Euler(map.transform.InverseTransformDirection(new Vector3(0f, astroHead.eulerAngles.y, 0f) - mapParent.eulerAngles)));

            packet.Write(map.transform.InverseTransformPoint(astroLeftHand.position));
            packet.Write(Quaternion.Euler(map.transform.InverseTransformDirection(astroLeftHand.eulerAngles) - mapParent.eulerAngles));

            packet.Write(map.transform.InverseTransformPoint(astroRightHand.position));
            packet.Write(Quaternion.Euler(map.transform.InverseTransformDirection(astroRightHand.eulerAngles) - mapParent.eulerAngles));

            MIKEServerManager.Main.SendData(ServiceType.Astronaut, packet, DeliveryType.Unreliable);
            yield return new WaitForSeconds(sendInterval);
        }
    }
}
