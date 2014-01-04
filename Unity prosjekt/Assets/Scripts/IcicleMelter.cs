using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;


class IcicleMelter : TemperatureEfect
{
    public bool Melting = false;
    public float MeltingSpeed = 0.1f;

    public Transform DripEfects;
    public Transform MeltEfects;

    void Start()
    {
        MeltEfects.gameObject.SetActive(false);
    }

    void Update()
    {
        if (Melting)
        {
            if (transform.localScale.y > 0)
            {
                transform.localScale -= new Vector3(0, MeltingSpeed, 0) * Time.deltaTime;
            }
            else
            {
                Deactivate();

            }
        }
    }

    public override void Activate()
    {
        Melting = true;
        MeltEfects.gameObject.SetActive(true);
        DripEfects.gameObject.SetActive(false);
    }

    public override void Deactivate()
    {
        Melting = false;
        MeltEfects.gameObject.SetActive(false);
        DripEfects.gameObject.SetActive(false);
    }

}

