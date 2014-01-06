using UnityEngine;
using System.Collections;

public class GunScript : MonoBehaviour
{

    public Transform muzzleObject;
    public Beam FreezeBeam;
    public Beam HeatBeam;

    public float FreezeTemperature;
    public float FreezeIntensity;

    public float HeatTemperature;
    public float HeatIntensity;

	// Use this for initialization
	void Start ()
	{
        FreezeBeam.SetEnabled(false);
        //HeatBeam.SetEnabled(false);
	}
	
	// Update is called once per frame
	void Update () {

	    if (Input.GetButton("Fire1") || Input.GetAxis("Fire1") >= 0.5f)
	    {
	        Ray ray = new Ray(Camera.main.transform.position, muzzleObject.forward);
	        Vector3 start = ray.origin;
	        Vector3 end = ray.GetPoint(100);
	        RaycastHit hit;
	        if (Physics.Raycast(ray, out hit))
	        {
                end = hit.point;
	            if (hit.transform.tag == "interactable")
	            {
                    TemperatureHandler temp = hit.transform.GetComponent<TemperatureHandler>();
	                if (temp)
	                {
	                    temp.ChangeTemperature(FreezeTemperature, FreezeIntensity);
	                }

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
            Ray ray = new Ray(Camera.main.transform.position, muzzleObject.forward);
            Vector3 start = ray.origin;
            Vector3 end = ray.GetPoint(100);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                end = hit.point;
                if (hit.transform.tag == "interactable")
                {
                    hit.transform.GetComponent<TemperatureHandler>().ChangeTemperature(HeatTemperature, HeatIntensity);
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
