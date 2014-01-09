using UnityEngine;
using System.Collections;

public class StartAnimation : Event
{

    public new Animation Animation;

    public override void Activate()
    {
        Animation.Play();
    }
}
