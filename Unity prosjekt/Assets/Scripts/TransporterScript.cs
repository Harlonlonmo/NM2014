using UnityEngine;
using System.Collections;

public class TransporterScript : MonoBehaviour {

    public string LevelName;
    public int SpawnPoint;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            TransportInfo.spawnPoint = SpawnPoint;
            TransportInfo.levelName = LevelName;
            Application.LoadLevel("Loading");
        }
    }
}
