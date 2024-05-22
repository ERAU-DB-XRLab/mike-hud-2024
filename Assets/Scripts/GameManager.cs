using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private MIKEIntro intro;
    [SerializeField] private MIKECalibration calibration;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(StartApplication());
    }

    private IEnumerator StartApplication()
    {
        intro.Play();
        yield return new WaitUntil(() => intro.Complete);
        yield return new WaitForSeconds(1f);
        if (PlayerPrefs.HasKey("useFailSafe"))
        {
            calibration.Calibrate(true);
            PlayerPrefs.DeleteKey("useFailSafe");
        }
        else
            calibration.Calibrate();
    }

    public void SetUseFailSafe()
    {
        PlayerPrefs.SetInt("useFailSafe", 1);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
