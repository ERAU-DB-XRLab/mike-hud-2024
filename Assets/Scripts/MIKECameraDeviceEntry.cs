using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class MIKECameraDeviceEntry : MIKEInputDeviceEntry
{

    [SerializeField] private GameObject data;
    [SerializeField] private RawImage img;

    private Texture2D tex;
    private ContentSizeFitter fitter;
    private float startingHeight = 115, endingHeight = 400;

    public bool b;

    new void Awake()
    {
        base.Awake();
        fitter = GetComponentInParent<ContentSizeFitter>();
    }

    // Start is called before the first frame update
    new void Start()
    {

        base.Start();
        data.SetActive(true);
        img.CrossFadeAlpha(0f, 0f, true);

    }

    public override void Init(int deviceID, string deviceType)
    {
        base.Init(deviceID, deviceType);
        tex = new Texture2D(1920, 960, TextureFormat.RGBA32, false);
        img.texture = tex;
    }

    // Update is called once per frame
    new void Update()
    {

        base.Update();

        if (b)
        {
            b = false;
            ButtonClicked();
        }

    }

    public override IEnumerator Expand()
    {

        RectTransform t = (RectTransform)transform;
        float width = t.sizeDelta.x;
        float timeToExpand = 0.3f;
        int stepCount = 20;

        img.CrossFadeAlpha(1f, timeToExpand * 1.5f, true);

        for (int i = 0; i < stepCount; i++)
        {
            yield return new WaitForSeconds(timeToExpand / stepCount);
            float newHeight = Mathf.Lerp(startingHeight, endingHeight, i / (float)stepCount);
            t.sizeDelta = new Vector2(width, newHeight);
            col.size = new Vector3(width, newHeight, .1f);
            img.transform.localScale = Vector3.Lerp(new Vector3(0.01f, 0.01f, 0.01f), new Vector3(0.2f, 0.2f, 0.2f), i / (float)stepCount);
            fitter.enabled = false;
            fitter.enabled = true;
        }

    }

    public override IEnumerator Contract()
    {

        RectTransform t = (RectTransform)transform;
        float width = t.sizeDelta.x;
        float timeToExpand = 0.3f;
        int stepCount = 20;

        img.CrossFadeAlpha(0f, timeToExpand * .5f, true);

        for (int i = 0; i < stepCount; i++)
        {
            yield return new WaitForSeconds(timeToExpand / stepCount);
            float newHeight = Mathf.Lerp(endingHeight, startingHeight, i / (float)stepCount);
            t.sizeDelta = new Vector2(width, newHeight);
            col.size = new Vector3(width, newHeight, .1f);
            img.transform.localScale = Vector3.Lerp(new Vector3(0.2f, 0.2f, 0.2f), new Vector3(0.01f, 0.01f, 0.01f), i / (float)stepCount);
            fitter.enabled = false;
            fitter.enabled = true;
        }

    }

    public override void ReceiveData(byte[] data)
    {

        base.ReceiveData(data);

        List<byte> jpgData = data.ToList();
        jpgData.RemoveAt(0);

        tex.LoadImage(jpgData.ToArray());
        tex.Apply(true, false);

    }

}
