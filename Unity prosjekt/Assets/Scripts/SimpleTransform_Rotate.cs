using UnityEngine;
using System.Collections;

public class SimpleTransform_Rotate : MonoBehaviour {

    public Vector3 direction;
    public float speed = 1f;
	
	// Update is called once per frame
	void Update () {
        transform.Rotate(direction * (Time.deltaTime * speed));
	}
}
