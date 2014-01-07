using UnityEngine;
using System.Collections;

public class SoundToggler: HitEfect
{

    public LoopingSound sound;

    public bool state = false;

    public override void Activate()
    {

        state = !state;
        if (state)
        {
            sound.Play();
        }
        else
        {
            sound.Stop();
        }
    }
}
