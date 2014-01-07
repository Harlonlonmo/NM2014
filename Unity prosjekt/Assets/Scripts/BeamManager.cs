using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BeamManager : Beam
{

    public bool BeamActive;
    public GameObject[] Beams;
    public GameObject[] ParticleSystems; 
    public GameObject MuzzleLight;
    public GameObject ImpactLight;

    private List<ParticleSystem> _pSystems; 

    public float ImpactLightOffset;

    void Awake()
    {
        _pSystems = new List<ParticleSystem>();
    }

    void Start()
    {
        BeamActive = false;
        foreach (var o in Beams)
            o.SetActive(false);

        foreach (var system in ParticleSystems)
        {
            _pSystems.Add(system.GetComponent<ParticleSystem>());
            _pSystems.Last().enableEmission = false; 
        }

        MuzzleLight.SetActive(false);
        ImpactLight.SetActive(false);
    }

    public override void SetEnabled(bool enabled)
    {
        foreach (var beam in Beams)
            beam.SetActive(enabled);

        foreach (ParticleSystem pSystem in _pSystems)
        {
            pSystem.enableEmission = enabled; 
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
        foreach (var receiver in Beams.SelectMany(beam => beam.GetComponent<LightningEmitter>().lightningReceivers))
        {
            receiver.transform.position = pos2;
        }
    }
}
