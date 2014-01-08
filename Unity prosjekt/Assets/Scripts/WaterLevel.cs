using UnityEngine;
using System.Collections;

public class WaterLevel : WaterTank
{

    public float waterPerMeter;



    public override void AddWater(float amount)
    {
        transform.Translate(Vector3.up*amount/waterPerMeter);
    }
}
