using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MIKESaveManager : MonoBehaviour
{
    [SerializeField] private MIKESettingsWidget m_SettingsWidget;
    [SerializeField] private bool loadOnStart;

    void OnEnable()
    {
        if(loadOnStart)
        Load();    
    }

    public void Load()
    {
        if (PlayerPrefs.HasKey("TSS-IP"))
        {
            string tssIP = PlayerPrefs.GetString("TSS-IP");
            m_SettingsWidget.SetTSSIP(tssIP);

            string lmccIP = PlayerPrefs.GetString("LMCC-IP");
            m_SettingsWidget.SetLMCCIP(lmccIP);

            Apply();

        } else
        {
            TSSManager.Main.SetHost("165.227.90.160");
        }
    }

    public void UseDefaultTelemetry()
    {
        m_SettingsWidget.SetTSSIP("165.227.90.160");
        Apply();
    }

    public void Apply()
    {
        TSSManager.Main.SetHost(m_SettingsWidget.GetTSSIP());
        MIKEServerManager.Main.SetEndPoint(m_SettingsWidget.GetLMCCIP());
        MIKENotificationManager.Main.SendNotification("IP UPDATE", "IPs Successfully Updated", Color.green, 2f);
        Save();
    }

    public void Save()
    {
        PlayerPrefs.SetString("TSS-IP", m_SettingsWidget.GetTSSIP());
        PlayerPrefs.SetString("LMCC-IP", m_SettingsWidget.GetLMCCIP());
    }

}
