using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CrystalBlast : TemperatureEfect {
    public bool BeamActive;
    public GameObject[] Beams;
    public GameObject[] ParticleSystems;

    public LoopingSound Sound;

    public GameObject MuzzleLight;
    public GameObject ImpactLight;

    public Transform TargetTransform; 

    private List<ParticleSystem> _pSystems;


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

    public override void Activate()
    {
        foreach (var beam in Beams)
        {
            beam.SetActive(enabled);
        }

        foreach (ParticleSystem pSystem in _pSystems)
        {
            pSystem.enableEmission = enabled;
        }


        MuzzleLight.SetActive(enabled);
        ImpactLight.SetActive(enabled);
        if (Sound)
        {
            if (enabled)
            {
                Sound.Play();
            }
            else
            {
                Sound.Stop();
            }
        }
    }

    void Update()
    {
        SetTarget(transform.position, TargetTransform.position);
        ImpactLight.transform.position = TargetTransform .position - (TargetTransform.position - transform.position).normalized;
    }

    public override void Deactivate()
    {

    }

    void SetTarget(Vector3 pos1, Vector3 pos2)
    {
        foreach (var receiver in Beams.SelectMany(beam => beam.GetComponent<LightningEmitter>().lightningReceivers))
        {
            receiver.transform.position = pos2;
        }
    }
}
