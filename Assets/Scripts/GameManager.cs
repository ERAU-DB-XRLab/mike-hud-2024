using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private MIKEIntro intro;
    [SerializeField] private MIKECalibration calibration;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(StartApplication());
    }

    // Update is called once per frame
    void Update()
    {

    }

    private IEnumerator StartApplication()
    {
        intro.Play();
        yield return new WaitUntil(() => intro.Complete);
        yield return new WaitForSeconds(1f);
        calibration.Calibrate();
    }
}
