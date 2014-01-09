using UnityEngine;
using System.Collections;

public class ActivateObject : Event
{

    public override void Activate()
    {
        gameObject.SetActive(true);
    }
}
