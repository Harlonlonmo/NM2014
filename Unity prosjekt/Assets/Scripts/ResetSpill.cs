using UnityEngine;
using System.Collections;

public class ResetSpill : Event {

    public override void Activate()
    {
        TransportInfo.spawnPoint = 0;
        GunData.Reset();
    }
}

public static class GunData
{

    public static bool hasHeat = false;
    public static bool hasFreeze = false;


    public static void Reset()
    {
        hasFreeze = false;
        hasHeat = false;
    }
}
