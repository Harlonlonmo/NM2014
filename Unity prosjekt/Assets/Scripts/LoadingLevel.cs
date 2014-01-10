using UnityEngine;
using System.Collections;

public class LoadingLevel : MonoBehaviour {

    void Start()
    {
        Application.LoadLevel(TransportInfo.levelName);
    }
}
