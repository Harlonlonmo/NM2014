using UnityEngine;
using System.Collections;

public class IceFloes : MonoBehaviour {
	
	// Update is called once per frame
	void Update () {
	    if (transform.position.y <= 0)
	    {
	        rigidbody.AddForce(Vector3.up*(transform.position.y*transform.position.y));
	    }
	}
}
