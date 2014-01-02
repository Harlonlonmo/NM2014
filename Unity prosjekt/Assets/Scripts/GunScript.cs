using UnityEngine;
using System.Collections;

public class GunScript : MonoBehaviour
{

    public Transform muzzleObject;
    public Beam beamObject;

	// Use this for initialization
	void Start ()
	{
        beamObject.enabled = false;
	}
	
	// Update is called once per frame
	void LateUpdate () {

	    if (Input.GetButton("Fire1") || Input.GetAxis("Fire1") >= 0.5f)
	    {
	        Ray ray = new Ray(muzzleObject.position, muzzleObject.forward);
	        Vector3 start = ray.origin;
	        Vector3 end = ray.GetPoint(100);
	        RaycastHit hit;
	        if (Physics.Raycast(ray, out hit))
	        {
                end = hit.point;
	            if (hit.transform.tag == "interacable")
	            {
	                hit.transform.GetComponent<TemperatureHandler>();
	            }
	        }
            beamObject.enabled = true;
	    }
	    else
	    {
            beamObject.enabled = false;
	    }
	}
}
