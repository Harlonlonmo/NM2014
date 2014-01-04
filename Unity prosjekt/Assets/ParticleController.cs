using UnityEngine;
using System.Collections;

public class ParticleController : MonoBehaviour
{

    public ParticleSystem[] particles;

    public void StartEmitting()
    {
        foreach (ParticleSystem p in particles)
        {
            p.enableEmission = true;
        }
    }

    public void StopEmitting()
    {
        foreach (ParticleSystem p in particles)
        {
            p.enableEmission = false;
        }
    }
}
