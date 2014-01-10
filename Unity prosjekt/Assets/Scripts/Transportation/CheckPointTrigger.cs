using UnityEngine;
using System.Collections;

public class CheckPointTrigger : MonoBehaviour
{

    public int CheckPointID;

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            TransportInfo.spawnPoint = CheckPointID;
        }
    }
}
