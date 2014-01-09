using System;
using UnityEngine;
using System.Linq;

[RequireComponent(typeof(Collider))]
public class GUI_Button : MonoBehaviour
{
    public Event[] UpEvents;

    public Event[] HoverEvents;
    public Event[] ExitEvents;

    public event Action Click;

    public bool Activated = true;
    public bool OnlyOnTop = false;

    private bool _active;
    private bool _aborted;

    public void Abort()
    {
        _aborted = true;
    }

    void OnMouseEnter()
    {
        foreach (Event t in HoverEvents)
        {
            t.Activate();
        }
    }

    void OnMouseExit()
    {
        foreach (Event t in ExitEvents)
        {
            t.Activate();
        }
    }

    void OnMouseDown()
    {
        _aborted = false;
    }

    void OnMouseUpAsButton()
    {
        if (!_aborted)
        {
            foreach (Event t in UpEvents)
            {
                t.Activate();
            }
            if (Click != null)
            {
                Click();
            }
        }
    }
}


