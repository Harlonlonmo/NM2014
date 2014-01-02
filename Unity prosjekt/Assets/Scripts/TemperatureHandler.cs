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

        public Action enterEvent;
        public Action exitEvent;
    }


    public float temperature;
    public temperatureRange[] temperatureRanges;

	void OnCollisionEnter(Collision col)
    {

	}

    public void changeTemperature(float target, float intrnsity)
    {
        temperature += (target - temperature);
    }
}
