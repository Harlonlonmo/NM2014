using UnityEngine;
using System.Collections;

public class GunScript : MonoBehaviour
{

    public Transform muzzleObject;
    public Beam FreezeBeam;
    public Beam HeatBeam;

	// Use this for initialization
	void Start ()
	{
        FreezeBeam.enabled = false;
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
            FreezeBeam.Render(start, end);
            FreezeBeam.SetEnabled(true);
	    }
	    else
	    {
            FreezeBeam.SetEnabled(false);
	    }

        if (Input.GetButton("Fire2") || Input.GetAxis("Fire2") >= 0.5f)
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
            HeatBeam.Render(start, end);
            HeatBeam.SetEnabled(true);
        }
        else
        {
            HeatBeam.SetEnabled(false);
        }
	}
}
