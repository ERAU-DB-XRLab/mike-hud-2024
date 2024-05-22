using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MIKECompass : MonoBehaviour
{

    public static MIKECompass Main { get; private set; }

    [SerializeField] private Transform bodyOrigin;
    [SerializeField] private MIKECalibration calibration;

    void Awake()
    {
        Main = this;    
    }

    // Start is called before the first frame update
    void Start()
    {
        TSSManager.Main.OnIMUUpdated += UpdateIMU;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = bodyOrigin.position;
    }

    public void UpdateIMU(IMUData data)
    {
        Debug.Log("Heading: " + data.YourEVA.heading);
        Vector3 forward = bodyOrigin.forward;
        forward.y = 0;
        transform.rotation = Quaternion.LookRotation(forward, Vector3.up);
        transform.Rotate(Vector3.up, (float)-data.YourEVA.heading);
    }

}
