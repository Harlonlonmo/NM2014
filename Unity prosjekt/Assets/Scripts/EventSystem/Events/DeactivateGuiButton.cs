using UnityEngine;
using System.Collections;

public class DeactivateGuiButton : Event
{

    public GUI_Button button;

    public override void Activate()
    {
        button.Activated = false;
    }
}
