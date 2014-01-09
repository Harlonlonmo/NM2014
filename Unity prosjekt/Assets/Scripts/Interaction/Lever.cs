using System.Collections;

public class Lever : HitEfect
{

    public bool userActivateable;
    public bool userDeactivateable;

    public HitEfect[] ActivateEfects;
    public HitEfect[] DeActivateEfects;



    public bool state;

    public LeverHandel handel;


    public void SetState(bool state)
    {
        this.state = state;
        handel.state = state;
        foreach (HitEfect e in state ? ActivateEfects : DeActivateEfects)
        {
            e.Activate();
        }

    }

    public override void Activate()
    {
        if (!state ? userActivateable : userDeactivateable)
        {
            SetState(!state);
        }
    }


}