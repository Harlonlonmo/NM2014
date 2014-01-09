using UnityEngine;
using System.Collections;

public class TransporterScript : MonoBehaviour {

    public string transportTo;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider other)
    {
        Application.LoadLevel(transportTo);
    }
}
