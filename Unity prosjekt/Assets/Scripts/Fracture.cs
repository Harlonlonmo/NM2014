using UnityEditor;
using UnityEngine;

public class Fracture : TemperatureEfect
{
    public SimpleFracture sFracture;
    public float FractureForce; 

    void Start()
    {
        // Initial fracture
        for (int i = 0; i < 5; i++)
        {
            
            sFracture.FractureAtPoint(collider.bounds.center, Vector3.down * FractureForce);
        }
    }

    public override void Activate()
    {
        // Show object 
    }

    public override void Deactivate()
    {
        // WarmUp/melt
    }
}
