using UnityEngine;
using System.Collections;

public class WaterTankPuzzle : WaterTank
{

    public float fullWaterLevel = 10f;

    public float prosent = 0f;
    private float WaterLevel = 0f;

    void Reset()
    {
        prosent = 0;
        WaterLevel = 0;
    }

    private Renderer[] subrenderers;

    void Start()
    {
        subrenderers = GetComponentsInChildren<Renderer>();
        Reset();
        UpdateWater();
    }

    private void UpdateWater()
    {
        prosent = WaterLevel/fullWaterLevel;
        transform.localScale = new Vector3(1, prosent, 1);
        if (prosent == 0)
        {
            foreach (Renderer r in subrenderers)
            {
                r.enabled = false;
            }
        }
        else
        {
            foreach (Renderer r in subrenderers)
            {
                r.enabled = true;
            }
        }
    }

    public override void AddWater(float amount)
    {
        WaterLevel += amount;
        UpdateWater();
    }
}
