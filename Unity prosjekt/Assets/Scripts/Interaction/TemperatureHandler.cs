using System;
using UnityEngine;
using System.Collections;

public class TemperatureHandler : MonoBehaviour
{

    [System.Serializable]
    public class temperatureRange
    {
        public float min;
        public float max;

        public bool Active { get; set; }

        public TemperatureEfect efect;
    }


    public float temperature;

    public float minTemperature;
    public float maxTemperature;

    public temperatureRange[] temperatureRanges;

    public void ChangeTemperature(float target, float intensity)
    {
        temperature += Math.Sign(target - temperature) * intensity * Time.deltaTime;

        temperature = Math.Min(maxTemperature, Math.Max(minTemperature, temperature));

        foreach (temperatureRange t in temperatureRanges)
        {
            if (temperature >= t.min && temperature <= t.max)
            {
                if (!t.Active)
                {
                    t.Active = true;
                    t.efect.Activate();
                }
            }
            else
            {
                if (t.Active)
                {
                    t.Active = false;
                    t.efect.Deactivate();
                }
            }
        }
    }
}
