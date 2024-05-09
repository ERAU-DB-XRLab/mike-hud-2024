using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MIKEThumbstickDeviceEntry : MIKEInputDeviceEntry
{

    [SerializeField] private TextMeshProUGUI xValueText, yValueText, selectValueText;
    [SerializeField] private GameObject data;
    [SerializeField] private Transform thumbstickMesh;

    private ContentSizeFitter fitter;
    private float startingHeight = 115, endingHeight = 315;
    private int zeroX = 28, zeroY = 28;

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
        // Initial values
        xValueText.CrossFadeAlpha(0f, 0f, true);
        yValueText.CrossFadeAlpha(0f, 0f, true);
        selectValueText.CrossFadeAlpha(0f, 0f, true);
        thumbstickMesh.parent.gameObject.SetActive(false);

        data.SetActive(true);

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

        thumbstickMesh.parent.gameObject.SetActive(true);

        xValueText.CrossFadeAlpha(1f, timeToExpand * 1.5f, true);
        yValueText.CrossFadeAlpha(1f, timeToExpand * 1.5f, true);
        selectValueText.CrossFadeAlpha(1f, timeToExpand * 1.5f, true);

        for (int i = 0; i < stepCount; i++)
        {
            yield return new WaitForSeconds(timeToExpand / stepCount);
            float newHeight = Mathf.Lerp(startingHeight, endingHeight, i / (float)stepCount);
            t.sizeDelta = new Vector2(width, newHeight);
            col.size = new Vector3(width, newHeight, .1f);
            thumbstickMesh.parent.localScale = Vector3.Lerp(new Vector3(0.01f, 0.01f, 0.01f), new Vector3(1f, 1f, 1f), i / (float) stepCount);
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

        xValueText.CrossFadeAlpha(0f, timeToExpand * .5f, true);
        yValueText.CrossFadeAlpha(0f, timeToExpand * .5f, true);
        selectValueText.CrossFadeAlpha(0f, timeToExpand * .5f, true);

        for (int i = 0; i < stepCount; i++)
        {
            yield return new WaitForSeconds(timeToExpand / stepCount);
            float newHeight = Mathf.Lerp(endingHeight, startingHeight, i / (float)stepCount);
            t.sizeDelta = new Vector2(width, newHeight);
            col.size = new Vector3(width, newHeight, .1f);
            thumbstickMesh.parent.localScale = Vector3.Lerp(new Vector3(1f, 1f, 1f), new Vector3(0.01f, 0.01f, 0.01f), i / (float)stepCount);
            fitter.enabled = false;
            fitter.enabled = true;
        }

        thumbstickMesh.parent.gameObject.SetActive(false);

    }

    public override void ReceiveData(byte[] data)
    {

        base.ReceiveData(data);
        int xValue = data[1] - zeroX;
        int yValue = data[2] - zeroY;
        int selectValue = data[3];

        xValueText.SetText("X Value: " + xValue);
        yValueText.SetText("Y Value: " + yValue);
        selectValueText.SetText("Select Value: " + selectValue);

        thumbstickMesh.localRotation = Quaternion.Euler(new Vector3(-yValue, 0, -xValue));

    }

}
