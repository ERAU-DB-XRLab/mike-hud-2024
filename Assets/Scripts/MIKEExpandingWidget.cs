using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class MIKEExpandingWidget : MIKEWidget
{

    private Vector3 defaultScale;
    private MaskableGraphic[] graphics;

    new void Awake()
    {
        base.Awake();
        defaultScale = transform.localScale;
        graphics = GetComponentsInChildren<MaskableGraphic>();
    }

    public override void Activate()
    {
        gameObject.SetActive(true);
        
        StopCoroutine("Expand");
        StartCoroutine(Expand());

        foreach(MaskableGraphic graphic in graphics)
        {
            graphic.CrossFadeAlpha(0f, 0f, true);
            graphic.CrossFadeAlpha(1f, MIKEResources.Main.WidgetExpandTime * 1.4f, true);
        }

    }

    public override void Deactivate()
    {
        base.Deactivate();
    }

    public IEnumerator Expand()
    {

        Vector3 fullSize = defaultScale;
        Vector3 startingWidth = new Vector3(0.1f, 0, 1f);
        Vector3 heightSize = new Vector3(0.1f, 1f, 1f);

        float steps = 15;


        for (int i = 0; i < (int)steps; i++)
        {
            yield return new WaitForSeconds(MIKEResources.Main.WidgetExpandTime / steps);
            transform.localScale = Vector3.Lerp(fullSize / 2, fullSize, (float)i / (steps - 1));
        }

        //for (int i = 0; i < (int)steps; i++)
        //{
        //    yield return new WaitForSeconds((MIKEResources.Main.WidgetExpandTime * 2 / 3) / steps);
        //    transform.localScale = Vector3.Lerp(startingWidth, heightSize, (float)i / (steps - 1));
        //}

        //for (int i = 0; i < (int)steps; i++)
        //{
        //    yield return new WaitForSeconds((MIKEResources.Main.WidgetExpandTime * 1 / 3) / steps);
        //    transform.localScale = Vector3.Lerp(heightSize, fullSize, (float)i / (steps - 1));
        //}

    }

}
