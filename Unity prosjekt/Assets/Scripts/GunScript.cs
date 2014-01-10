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

    public float interactionDistance = 5.0f;



    // Use this for initialization
    void Start()
    {
        FreezeBeam.SetEnabled(false);
        HeatBeam.SetEnabled(false);
    }

    // Update is called once per frame
    void Update()
    {
        RayType type = RayType.Hover;
        if (Input.GetButton("Fire1") || Input.GetAxis("Fire1") >= 0.5f)
        {
            type = RayType.Heat;
        }
        else if (Input.GetButton("Fire2") || Input.GetAxis("Fire2") >= 0.5f)
        {
            type = RayType.Freeze;
        }
        else if (Input.GetButtonDown("Interact"))
        {
            type = RayType.Interact;
        }

        Ray ray = new Ray(Camera.main.transform.position, muzzleObject.forward);
        Vector3 start = ray.origin;
        Vector3 end = ray.GetPoint(100);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            //Debug.Log("Raycast hit : " + hit.transform.gameObject.name);
            end = hit.point;

            if (hit.transform.tag == "interactable")
            {


                if (type == RayType.Freeze || type == RayType.Heat)
                {
                    TemperatureHandler temp = hit.transform.GetComponent<TemperatureHandler>();
                    if (temp)
                    {
                        float target = type == RayType.Freeze ? FreezeTemperature : HeatTemperature;
                        float intensity = type == RayType.Freeze ? FreezeIntensity : HeatIntensity;
                        temp.ChangeTemperature(target, intensity);
                    }
                }

                bool jump = false;
                if (type == RayType.Interact || type == RayType.Hover)
                {
                    jump = hit.distance > interactionDistance;
                }

                if (!jump)
                {
                    HitHandler hitHand = hit.transform.GetComponent<HitHandler>();
                    if (hitHand)
                    {
                        hitHand.Hit(hit, type);
                    }
                }
            }
        }
        if (type == RayType.Freeze)
        {
            FreezeBeam.Render(start, end);
            FreezeBeam.SetEnabled(true);
        }
        else
        {
            FreezeBeam.SetEnabled(false);
        }
        if (type == RayType.Heat)
        {
            HeatBeam.Render(start, end);
            HeatBeam.SetEnabled(true);
        }
        else
        {
            HeatBeam.SetEnabled(false);
        }
    }
}
