using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MIKEVitalsWidget : MIKEExpandingWidget
{
    [Header("Icons")]
    [SerializeField] private MIKEIconWidgetValue heartRateIcon;
    [SerializeField] private MIKEIconWidgetValue o2StorageIcon;
    [SerializeField] private MIKEIconWidgetValue suitPSIIcon;
    [SerializeField] private MIKEIconWidgetValue o2PSIIcon;
    [SerializeField] private MIKEIconWidgetValue co2PSIIcon;
    [SerializeField] private MIKEIconWidgetValue otherPSIIcon;
    [SerializeField] private MIKEIconWidgetValue fanRPMIcon;
    [SerializeField] private MIKEIconWidgetValue temperatureIcon;
    [SerializeField] private MIKEIconWidgetValue helmetCO2Icon;
    [SerializeField] private MIKEIconWidgetValue scrubberCO2StorageIcon;

    [Header("Resources")]
    [SerializeField] private MIKEVitalsWidgetValue batteryTimeLeft;
    [SerializeField] private MIKEVitalsWidgetValue O2timeLeft;
    [SerializeField] private MIKEVitalsWidgetValue O2PrimaryStorage;
    [SerializeField] private MIKEVitalsWidgetValue O2PrimaryPressure;
    [SerializeField] private MIKEVitalsWidgetValue O2SecondaryStorage;
    [SerializeField] private MIKEVitalsWidgetValue O2SecondaryPressure;
    [SerializeField] private MIKEVitalsWidgetValue coolant;

    [Header("Suit")]
    [SerializeField] private MIKEVitalsWidgetValue heartRate;
    [SerializeField] private MIKEVitalsWidgetValue O2Consumption;
    [SerializeField] private MIKEVitalsWidgetValue CO2Production;
    [SerializeField] private MIKEVitalsWidgetValue suitO2Pressure;
    [SerializeField] private MIKEVitalsWidgetValue suitCO2Pressure;
    [SerializeField] private MIKEVitalsWidgetValue suitOtherPressure;
    [SerializeField] private MIKEVitalsWidgetValue suitTotalPressure;
    [SerializeField] private MIKEVitalsWidgetValue helmetCO2Pressure;

    [Header("Fans")]
    [SerializeField] private MIKEVitalsWidgetValue fanPrimary;
    [SerializeField] private MIKEVitalsWidgetValue fanSecondary;

    [Header("Misc")]
    [SerializeField] private MIKEVitalsWidgetValue scrubberAStorage;
    [SerializeField] private MIKEVitalsWidgetValue scrubberBStorage;
    [SerializeField] private MIKEVitalsWidgetValue temperature;
    [SerializeField] private MIKEVitalsWidgetValue coolantGasPressure;
    [SerializeField] private MIKEVitalsWidgetValue coolantLiquidPressure;


    // Start is called before the first frame update
    void Start()
    {
        SetWidgetBounds();
        TSSManager.Main.OnTelemetryUpdated += HandleVitalsData;
    }

    private void HandleVitalsData(TelemetryData data)
    {
        UpdateVitals(data);
        UpdateIconVitals(data);
    }

    private void UpdateVitals(TelemetryData data)
    {
        batteryTimeLeft.SetValue((float)data.YourEVA.batt_time_left);
        O2timeLeft.SetValue(data.YourEVA.oxy_time_left);
        O2PrimaryStorage.SetValue((float)data.YourEVA.oxy_pri_storage);
        O2PrimaryPressure.SetValue((float)data.YourEVA.oxy_pri_pressure);
        O2SecondaryStorage.SetValue((float)data.YourEVA.oxy_sec_storage);
        O2SecondaryPressure.SetValue((float)data.YourEVA.oxy_sec_pressure);
        coolant.SetValue((float)data.YourEVA.coolant_ml);

        heartRate.SetValue((float)data.YourEVA.heart_rate);
        O2Consumption.SetValue((float)data.YourEVA.oxy_consumption);
        CO2Production.SetValue((float)data.YourEVA.co2_production);
        suitO2Pressure.SetValue((float)data.YourEVA.suit_pressure_oxy);
        suitCO2Pressure.SetValue((float)data.YourEVA.suit_pressure_co2);
        suitOtherPressure.SetValue((float)data.YourEVA.suit_pressure_other);
        suitTotalPressure.SetValue((float)data.YourEVA.suit_pressure_total);
        helmetCO2Pressure.SetValue((float)data.YourEVA.helmet_pressure_co2);

        fanPrimary.SetValue((float)data.YourEVA.fan_pri_rpm);
        fanSecondary.SetValue((float)data.YourEVA.fan_sec_rpm);

        scrubberAStorage.SetValue((float)data.YourEVA.scrubber_a_co2_storage);
        scrubberBStorage.SetValue((float)data.YourEVA.scrubber_b_co2_storage);
        temperature.SetValue((float)data.YourEVA.temperature);
        coolantGasPressure.SetValue((float)data.YourEVA.coolant_gas_pressure);
        coolantLiquidPressure.SetValue((float)data.YourEVA.coolant_liquid_pressure);
    }

    private void UpdateIconVitals(TelemetryData data)
    {
        heartRateIcon.SetValue((float)data.YourEVA.heart_rate);
        o2StorageIcon.SetValue(MIKESystemManager.Main.SystemStatuses[SystemType.Oxygen].GetActiveStatus() == "Primary Tank" ? (float)data.YourEVA.oxy_pri_storage : (float)data.YourEVA.oxy_sec_storage);
        suitPSIIcon.SetValue((float)data.YourEVA.suit_pressure_total);
        o2PSIIcon.SetValue((float)data.YourEVA.suit_pressure_oxy);
        co2PSIIcon.SetValue((float)data.YourEVA.suit_pressure_co2);
        otherPSIIcon.SetValue((float)data.YourEVA.suit_pressure_other);
        fanRPMIcon.SetValue(MIKESystemManager.Main.SystemStatuses[SystemType.Fan].GetActiveStatus() == "Primary Fan" ? (float)data.YourEVA.fan_pri_rpm / 1000f : (float)data.YourEVA.fan_sec_rpm / 1000f);
        temperatureIcon.SetValue((float)data.YourEVA.temperature);
        helmetCO2Icon.SetValue((float)data.YourEVA.helmet_pressure_co2);
        scrubberCO2StorageIcon.SetValue(MIKESystemManager.Main.SystemStatuses[SystemType.CO2].GetActiveStatus() == "Scrubber A" ? (float)data.YourEVA.scrubber_a_co2_storage : (float)data.YourEVA.scrubber_b_co2_storage);
    }

    private void SetWidgetBounds()
    {
        batteryTimeLeft.SetBounds(3600f, 10800f);
        O2PrimaryStorage.SetBounds(20f, 100f);
        O2SecondaryStorage.SetBounds(20f, 100f);
        O2PrimaryPressure.SetBounds(600f, 3000f);
        O2SecondaryPressure.SetBounds(600f, 3000f);
        O2timeLeft.SetBounds(3600f, 21600f);
        coolant.SetBounds(80f, 100f, 100f);

        heartRate.SetBounds(50f, 160f, 90f);
        O2Consumption.SetBounds(0.05f, 0.15f, 0.1f);
        CO2Production.SetBounds(0.05f, 0.15f, 0.1f);
        suitO2Pressure.SetBounds(3.5f, 4.1f, 4.0f);
        suitCO2Pressure.SetBounds(0.0f, 0.1f, 0.0f);
        suitOtherPressure.SetBounds(0.0f, 0.5f, 0.0f);
        suitTotalPressure.SetBounds(3.5f, 4.5f, 4.0f);
        helmetCO2Pressure.SetBounds(0.0f, 0.15f, 0.1f);

        fanPrimary.SetBounds(20000f, 30000f, 30001f);
        fanSecondary.SetBounds(20000f, 30000f, 30001f);

        scrubberAStorage.SetBounds(0f, 60f);
        scrubberBStorage.SetBounds(0f, 60f);
        temperature.SetBounds(50f, 90f, 70f);
        coolantLiquidPressure.SetBounds(100f, 700f, 500f);
        coolantGasPressure.SetBounds(0f, 700f, 0f);
    }
}
