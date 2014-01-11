using UnityEngine;
using System.Collections;

public class ChargeEffect : MonoBehaviour
{
    public ParticleSystem[] ParticleSystems;
    public TemperatureHandler TempHandler;
    public Action action; 
    private float currentTemp, lastTemp;
    

    void Start()
    {
        lastTemp = TempHandler.temperature;
        foreach (ParticleSystem system in ParticleSystems)
        {
            system.enableEmission = false;
        } 
    }

    public void Activate()
    {
        foreach (ParticleSystem system in ParticleSystems)
        {
            system.enableEmission = true; 
        }
    }

    void Update()
    {
        currentTemp = TempHandler.temperature;
        switch (action)
        {
            case Action.ToFreese: 
                if(currentTemp < lastTemp) Activate();
                else Deactivate();
                break; 
            case Action.ToMelt: 
                if (currentTemp > lastTemp) Activate();
                else Deactivate();
                break; 
        }

        lastTemp = TempHandler.temperature; 
    }

    public void Deactivate()
    {
        foreach (ParticleSystem system in ParticleSystems)
        {
            system.enableEmission = false;
        }
    }
}
