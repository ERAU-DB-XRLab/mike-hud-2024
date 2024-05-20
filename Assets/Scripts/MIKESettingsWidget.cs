using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MIKESettingsWidget : MIKEExpandingWidget
{

    [SerializeField] private TMP_InputField tss, lmcc;
    [SerializeField] private TMP_Text evText;
    private TMP_InputField selectedInputField;
    
    // Start is called before the first frame update
    protected new void Awake()
    {
        base.Awake();
    }

    void Start()
    {
        if (PlayerPrefs.HasKey("TSS-IP"))
        {
            tss.text = PlayerPrefs.GetString("TSS-IP");
            lmcc.text = PlayerPrefs.GetString("LMCC-IP");
        }
    }

    public void SwapEV()
    {
        if(TSSManager.Main.CurrentEVA == EVA.EVA1)
        {
            TSSManager.Main.CurrentEVA = EVA.EVA2;
            evText.text = "EV2";
        } else
        {
            TSSManager.Main.CurrentEVA = EVA.EVA1;
            evText.text = "EV1";
        }
    }

    public void SelectInputField(TMP_InputField inputField)
    {
        this.selectedInputField = inputField;
    }

    public void KeypadEnter(string data)
    {

        if(selectedInputField != null)
        {
            if (data.Equals("-"))
            {
                string text = selectedInputField.text;
                if(text.Length > 0)
                {
                    selectedInputField.text = text.Substring(0, text.Length - 1);
                }
            } else
            {
                selectedInputField.text += data;
            }
        }
        
    }

    private void OnDisable()
    {
        PlayerPrefs.SetString("TSS-IP", tss.text);
        PlayerPrefs.SetString("LMCC-IP", lmcc.text);
    }



}
