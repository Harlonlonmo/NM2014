using UnityEngine;
using System.Collections;

public class TemperatureFallback : TemperatureEfect
{

    public TemperatureHandler tempHandler;

    public float targetTemperature;
    public float intensity;

    public bool Active = false;

	// Use this for initialization
	void Start ()
	{
	}
	
	// Update is called once per frame
	void Update () {
	    if (Active)
	    {
            tempHandler.ChangeTemperature(targetTemperature, intensity);	        
	    }
	}

    public override void Activate()
    {
        Active = true;
    }

    public override void Deactivate()
    {
        Active = false;
    }
}
