using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;


class IcicleMelter : TemperatureEfect
{
    public bool Melting = false;
    public float MeltingSpeed = 0.1f;

    public float waterVolume;

    public WaterTank waterDestination;

    public ParticleController DripEfects;
    public ParticleController MeltEfects;

    public bool Dead = false;

    private float prosent = 1;

    void Start()
    {
        MeltEfects.StopEmitting();
    }

    void Update()
    {
        if (Melting)
        {
            if (prosent > 0)
            {
                float old = prosent;
                prosent -= MeltingSpeed * Time.deltaTime;
                prosent = Math.Min(1, Math.Max(0, prosent));
                if (waterDestination)
                {
                    waterDestination.AddWater((old-prosent) * waterVolume);
                }
                transform.localScale = new Vector3(1, prosent, 1);
            }
            else
            {
                Stop();
            }
        }
    }

    public override void Activate()
    {
        if (!Dead)
        {
            Melting = true;
            MeltEfects.StartEmitting();
            DripEfects.StopEmitting();
        }
    }

    public override void Deactivate()
    {
        if (!Dead)
        {
            Melting = false;
            MeltEfects.StopEmitting();
            DripEfects.StartEmitting();
        }
    }

    private void Stop()
    {

        Melting = false;
        MeltEfects.StopEmitting();
        DripEfects.StopEmitting();
        Dead = true;
    }

}

