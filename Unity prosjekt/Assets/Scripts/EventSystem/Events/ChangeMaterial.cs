using UnityEngine;
using System.Collections;

public class ChangeMaterial : Event
{

    public Material material;

    public override void Activate()
    {
        renderer.material = material;
    }
}
