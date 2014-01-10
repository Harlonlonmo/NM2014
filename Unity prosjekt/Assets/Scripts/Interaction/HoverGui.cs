using UnityEngine;
using System.Collections;

public class HoverGui : HitEfect
{

    public GUIText gui;
    public string text = "Default";

    public bool show = false;

    void Start()
    {
        gui.enabled = false;
    }


    //TODO fix multiuse bug
    void LateUpdate()
    {
        if(!show)
            gui.enabled = false;
        show = false;
    }

    public override void Activate()
    {
        show = true;
        gui.text = text;
        gui.enabled = true;
    }

    
}
