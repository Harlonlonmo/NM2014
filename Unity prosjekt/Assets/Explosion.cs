using UnityEngine;

public class Explosion : TemperatureEfect
{

    public GameObject[] Explosions;
    public GameObject toDestroy;  

    void Awake()
    {
        foreach (var ex in Explosions)
        {
            ex.SetActive(false);
        }
    }
    public override void Activate()
    {
        foreach (var ex in Explosions)
        {
            ex.SetActive(true);
            if(toDestroy != null)
                toDestroy.SetActive(false);
        }
    }

    public override void Deactivate()
    {
        foreach (var ex in Explosions)
        {
            ex.SetActive(false);
        }
    }
}
