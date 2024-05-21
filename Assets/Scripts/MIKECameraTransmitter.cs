using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.MagicLeap;

public class MIKECameraTransmitter : MonoBehaviour
{
    public static MIKECameraTransmitter Main { get; private set; }

    private WebCamTexture webCamTexture;

    public RawImage img;

    [SerializeField] private float framesPerSecond = 15;
    [SerializeField] private int quality = 15;
    [SerializeField] private int width = 640;
    [SerializeField] private int height = 480;

    [Header("DEBUG")]
    [SerializeField] private bool debugMode = false;
    [SerializeField] private string deviceName;

    public RawImage test;

    void Awake()
    {
        if (Main == null)
            Main = this;
        else
            Destroy(this);


    } 

    // Start is called before the first frame update
    void Start()
    {
        webCamTexture = new WebCamTexture();

        if (debugMode)
        {
            foreach (var device in WebCamTexture.devices)
            {
                Debug.Log("Camera Device: " + device.name);
            }
        }

        webCamTexture.deviceName = debugMode ? deviceName : WebCamTexture.devices[0].name;
        webCamTexture.Play();

        StartCoroutine(UpdateCameraTexture());
    }

    private IEnumerator UpdateCameraTexture()
    {
        while (true)
        {
            if (webCamTexture.isPlaying)
            {
                Texture2D t = new Texture2D(webCamTexture.width, webCamTexture.height);
                t.SetPixels(webCamTexture.GetPixels());
                t.Apply();
                var packet = new MIKEPacket(t.EncodeToJPG(quality));

                MIKEServerManager.Main.SendData(ServiceType.Camera, packet, DeliveryType.Unreliable);
                Debug.Log(packet.ByteArray.Length);
                yield return new WaitForSeconds(1f / framesPerSecond);
            }
            else
                yield return null;
        }
    }
}
