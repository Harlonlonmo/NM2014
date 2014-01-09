using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public class Timer : Event
{
    public bool AutoStart = true;
    public float Delay;
    public Event Event;

	// Use this for initialization
	void Start () {
	    if (AutoStart)
	    {
	        Activate();
	    }
	}

    public override void Activate()
    {
        if (Delay > 0)
        {
            StartCoroutine(startAsync());
        }
        else
        {
            Event.Activate();
        }
    }

    private IEnumerator startAsync()
    {
        yield return new WaitForSeconds(Delay);
        Event.Activate();
    }
}
