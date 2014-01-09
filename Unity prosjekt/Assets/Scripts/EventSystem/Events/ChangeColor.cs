using UnityEngine;

public class ChangeColor : Event
{
    public Color Color;

    public override void Activate()
    {
        renderer.material.color = Color;
    }
}
