using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MIKECameraTransmitter : MonoBehaviour
{
    public static MIKECameraTransmitter Main { get; private set; }

    private WebCamTexture webCamTexture;

    [SerializeField] private int framesPerSecond = 15;
    [SerializeField] private int quality = 15;
    [SerializeField] private int width = 640;
    [SerializeField] private int height = 480;

    [Header("DEBUG")]
    [SerializeField] private bool debugMode = false;
    [SerializeField] private string deviceName;

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
                Texture2D t = new Texture2D(width, height);
                t.SetPixels(webCamTexture.GetPixels());
                var packet = new MIKEPacket(t.EncodeToJPG(quality));
                MIKEServerManager.Main.SendData(ServiceType.Camera, packet, DeliveryType.Unreliable);

                yield return new WaitForSeconds(1f / framesPerSecond);
            }
            else
                yield return null;
        }
    }
}
