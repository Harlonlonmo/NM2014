using UnityEngine;

public class HitHandler : MonoBehaviour
{
    public HitEfect[] heatEfects;
    public HitEfect[] freezeEfects;
    public void Hit(RaycastHit hit, bool freeze)
    {

        foreach (HitEfect e in freeze ? freezeEfects : heatEfects)
        {
            e.Activate();
        }

    }
}
