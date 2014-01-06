using UnityEngine;

public enum RayType
{
    Freeze,
    Heat,
    Interact,
    None
}

public class HitHandler : MonoBehaviour
{
    public HitEfect[] heatEfects;
    public HitEfect[] freezeEfects;
    public HitEfect[] InteractEfects;
    public void Hit(RaycastHit hit, RayType rayType)
    {
        HitEfect[] effects;
        switch (rayType)
        {
            case RayType.Freeze:
                effects = freezeEfects;
                break;
            case RayType.Heat:
                effects = heatEfects;
                break;
            case RayType.Interact:
                effects = InteractEfects;
                break;
            default:
                return;
        }
        foreach (HitEfect e in effects)
        {
            e.Activate();
        }
    }
}
