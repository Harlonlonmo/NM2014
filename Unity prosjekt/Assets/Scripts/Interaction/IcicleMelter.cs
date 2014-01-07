using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

[System.Serializable]
class Scaling
{
    public bool X;
    public bool Y;
    public bool Z;
}

class IcicleMelter : TemperatureEfect
{
    public Scaling Scaling;

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
        if (MeltEfects)
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
                    waterDestination.AddWater((old - prosent) * waterVolume);
                }
                transform.localScale = new Vector3(Scaling.X ? prosent : 1, Scaling.Y ? prosent : 1, Scaling.Z ? prosent : 1);
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
            if (MeltEfects)
                MeltEfects.StartEmitting();
            if (DripEfects)
                DripEfects.StopEmitting();
        }
    }

    public override void Deactivate()
    {
        if (!Dead)
        {
            Melting = false;
            if (MeltEfects)
                MeltEfects.StopEmitting();
            if (DripEfects)
                DripEfects.StartEmitting();
        }
    }

    private void Stop()
    {

        Melting = false;
        if (MeltEfects)
            MeltEfects.StopEmitting();
        if (DripEfects)
            DripEfects.StopEmitting();
        Dead = true;
    }

}

