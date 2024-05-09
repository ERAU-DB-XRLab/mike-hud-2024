using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MIKEVitalsWidget : MIKEExpandingWidget
{

    [Header("Oxygen")]
    [SerializeField] private MIKEWidgetValue O2timeLeft;
    [SerializeField] private MIKEWidgetValue O2PrimaryStorage;
    [SerializeField] private MIKEWidgetValue O2PrimaryPressure;
    [SerializeField] private MIKEWidgetValue O2SecondaryStorage;
    [SerializeField] private MIKEWidgetValue O2SecondaryPressure;
    [SerializeField] private MIKEWidgetValue O2Consumption;
    [SerializeField] private MIKEWidgetValue CO2Production;

    [Header("Suit")]
    [SerializeField] private MIKEWidgetValue suitO2Pressure;
    [SerializeField] private MIKEWidgetValue suitCO2Pressure;
    [SerializeField] private MIKEWidgetValue suitOtherPressure;
    [SerializeField] private MIKEWidgetValue suitTotalPressure;
    [SerializeField] private MIKEWidgetValue helmetCO2Pressure;
    [SerializeField] private MIKEWidgetValue scrubberAPressure;
    [SerializeField] private MIKEWidgetValue scrubberBPressure;
    [SerializeField] private MIKEWidgetValue H20GasPressure;
    [SerializeField] private MIKEWidgetValue H20LiquidPressure;

    [Header("Vitals")]
    [SerializeField] private MIKEWidgetValue heartRate;

    [Header("Thermal")]
    [SerializeField] private MIKEWidgetValue temperature;
    [SerializeField] private MIKEWidgetValue coolant;

    [Header("Fan")]
    [SerializeField] private MIKEWidgetValue fanPrimary;
    [SerializeField] private MIKEWidgetValue fanSecondary;

    // Start is called before the first frame update
    void Start()
    {
        TSSManager.Main.OnTelemetryUpdated += UpdateVitals;
    }

    private void UpdateVitals(TelemetryData data)
    {
        /*O2timeLeft.SetValue(TSSManager.Main.TelemetryData.oxy_time_left);
        O2PrimaryStorage.SetValue((float)TSSManager.Main.TelemetryData.oxy_pri_storage);
        O2PrimaryPressure.SetValue((float)TSSManager.Main.TelemetryData.oxy_pri_pressure);
        O2SecondaryStorage.SetValue((float)TSSManager.Main.TelemetryData.oxy_sec_storage);
        O2SecondaryPressure.SetValue((float)TSSManager.Main.TelemetryData.oxy_sec_pressure);
        O2Consumption.SetValue((float)TSSManager.Main.TelemetryData.oxy_consumption);
        CO2Production.SetValue((float)TSSManager.Main.TelemetryData.co2_production);

        suitO2Pressure.SetValue((float)TSSManager.Main.TelemetryData.suit_pressure_oxy);
        suitCO2Pressure.SetValue((float)TSSManager.Main.TelemetryData.suit_pressure_co2);
        suitOtherPressure.SetValue((float)TSSManager.Main.TelemetryData.suit_pressure_other);
        suitTotalPressure.SetValue((float)TSSManager.Main.TelemetryData.suit_pressure_total);
        helmetCO2Pressure.SetValue((float)TSSManager.Main.TelemetryData.helmet_pressure_co2);
        scrubberAPressure.SetValue((float)TSSManager.Main.TelemetryData.scrubber_a_co2_storage);
        scrubberBPressure.SetValue((float)TSSManager.Main.TelemetryData.scrubber_b_co2_storage);
        H20GasPressure.SetValue((float)TSSManager.Main.TelemetryData.coolant_gas_pressure);
        H20LiquidPressure.SetValue((float)TSSManager.Main.TelemetryData.coolant_liquid_pressure);

        heartRate.SetValue((float)TSSManager.Main.TelemetryData.heart_rate);

        temperature.SetValue((float)TSSManager.Main.TelemetryData.temperature);
        coolant.SetValue((float)TSSManager.Main.TelemetryData.coolant_ml);

        fanPrimary.SetValue((float)TSSManager.Main.TelemetryData.fan_pri_rpm);
        fanSecondary.SetValue((float)TSSManager.Main.TelemetryData.fan_sec_rpm);*/

    }

}
