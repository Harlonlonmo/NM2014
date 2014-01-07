using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System.Collections;

public class BeamManager : Beam
{

    public bool BeamActive;
    public GameObject[] Beams;
    public GameObject MuzzleLight;
    public GameObject ImpactLight;

    public float ImpactLightOffset;

    private void Start()
    {
        BeamActive = false;
        foreach (var o in Beams)
            o.SetActive(false);

        MuzzleLight.SetActive(false);
        ImpactLight.SetActive(false);
    }

    public override void SetEnabled(bool enabled)
    {
        foreach (var beam in Beams)
        {
            beam.SetActive(enabled);
        }

        MuzzleLight.SetActive(enabled);
        ImpactLight.SetActive(enabled);
    }

    public override void Render(Vector3 start, Vector3 end)
    {
        SetTarget(start, end);
        ImpactLight.transform.position = end - (end-start).normalized * ImpactLightOffset; 
    }

    void SetTarget(Vector3 pos1, Vector3 pos2)
    {
        var direction = pos2 - pos1;
        foreach (var receiver in Beams.SelectMany(beam => beam.GetComponent<LightningEmitter>().lightningReceivers))
        {
            receiver.transform.position = pos2;
        }
    }
}
