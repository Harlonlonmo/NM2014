using UnityEngine;
using System.Collections;

public class DeathZoneTrigger : MonoBehaviour {

    void OnTriggerEnter(Collider other)
    {
        Application.LoadLevel(Application.loadedLevel);
    }
}
