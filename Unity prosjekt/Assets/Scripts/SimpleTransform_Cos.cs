using UnityEngine;
using System.Collections;

public class SimpleTransform_Cos : MonoBehaviour {

    public Vector3 direction;
    public float amplitude = 1f;
    public float speed = 1f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        transform.position += direction * (amplitude * Mathf.Cos(Time.time * speed));
	}
}
