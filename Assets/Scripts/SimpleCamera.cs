using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.MagicLeap;
using static UnityEngine.XR.MagicLeap.MLCameraBase;
using Renderer = UnityEngine.Renderer;
public class SimpleCamera : MonoBehaviour
{

    MLCamera.CaptureConfig _captureConfig;
    //Cached version of the MLCamera instance.
    MLCamera _camera;
    //The camera capture state
    bool _isCapturing;

    public RawImage t;
    private Texture2D videoTextureRGB;
    public Renderer _screenRendererRGB = null;

    private void Start()
    {
        t.texture = videoTextureRGB;
        videoTextureRGB = new Texture2D(640, 320);
        StartVideoCapture();
    }

    //Assumes that the capture configure was created with a Video CaptureType
    private void StartVideoCapture()
    {
        MLResult result = _camera.PrepareCapture(_captureConfig, out MLCamera.Metadata metaData);
        if (result.IsOk)
        {
            // Trigger auto exposure and auto white balance
            _camera.PreCaptureAEAWB();
            // Starts video capture. This call can also be called asynchronously 
            // Images capture uses the CaptureImage function instead.
            result = _camera.CaptureVideoStart();
            if (result.IsOk)
            {
                Debug.Log("Video capture started!");
                _isCapturing = true;
            }
            else
            {
                Debug.LogError("Failed to start video capture!");
            }
        }
    }

    void RawVideoFrameAvailable(MLCamera.CameraOutput output, MLCamera.ResultExtras extras, MLCamera.Metadata metadataHandle)
    {
        if (output.Format == MLCamera.OutputFormat.RGBA_8888)
        {
            UpdateRGBTexture(ref videoTextureRGB, output.Planes[0], _screenRendererRGB);
        }
    }

    private void UpdateRGBTexture(ref Texture2D videoTextureRGB, MLCamera.PlaneInfo imagePlane, Renderer renderer)
    {

        if (videoTextureRGB != null &&
            (videoTextureRGB.width != imagePlane.Width || videoTextureRGB.height != imagePlane.Height))
        {
            Destroy(videoTextureRGB);
            videoTextureRGB = null;
        }

        if (videoTextureRGB == null)
        {
            videoTextureRGB = new Texture2D((int)imagePlane.Width, (int)imagePlane.Height, TextureFormat.RGBA32, false);
            videoTextureRGB.filterMode = FilterMode.Bilinear;

            Material material = renderer.material;
            material.mainTexture = videoTextureRGB;
            material.mainTextureScale = new Vector2(1.0f, -1.0f);
        }

        int actualWidth = (int)(imagePlane.Width * imagePlane.PixelStride);

        if (imagePlane.Stride != actualWidth)
        {
            var newTextureChannel = new byte[actualWidth * imagePlane.Height];
            for (int i = 0; i < imagePlane.Height; i++)
            {
                Buffer.BlockCopy(imagePlane.Data, (int)(i * imagePlane.Stride), newTextureChannel, i * actualWidth, actualWidth);
            }
            videoTextureRGB.LoadRawTextureData(newTextureChannel);
        }
        else
        {
            videoTextureRGB.LoadRawTextureData(imagePlane.Data);
        }

        videoTextureRGB.Apply();
    }

}

