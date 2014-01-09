using UnityEngine;
using System.Collections;

public class Factory : HitEfect
{

    public TransportScript[] transporters;

    public Lever lever;

    public float timer;

    private float _timer;

    public bool active;

    public override void Activate()
    {
        _timer = timer;
        active = true;
        foreach (TransportScript t in transporters)
        {
            t.Activate();
        }
    }

    void Update()
    {
        if (active)
        {
            _timer -= Time.deltaTime;


            if (_timer <= 0)
            {
                active = false;
                foreach (TransportScript t in transporters)
                {
                    t.Deactivate();
                }
                lever.SetState(false);
            }

        }
    }
}
