using UnityEngine;
using System.Collections;

public class FreezeHandler : TemperatureEfect
{
    private MeshRenderer _mesh;

    void Start()
    {
        _mesh = GetComponent<MeshRenderer>();
        _mesh.enabled = false; 
        gameObject.layer = 4; 
    }

    public override void Activate()
    {
        _mesh.enabled = true;
        gameObject.layer = 10;
        Debug.Log("frozen");
    }

    public override void Deactivate()
    {

    }
}
