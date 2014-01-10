using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum Action
{
    ToFreese, 
    ToMelt
}

public class CrystalBlast : TemperatureEfect
{
    public bool BeamActive;
    public GameObject[] Beams;
    public GameObject[] ParticleSystems;
    public GameObject[] ToAffect;
    public LoopingSound Sound;
    public Action action; 

    public GameObject MuzzleLight;
    public GameObject ImpactLight;

    public Transform TargetTransform;

    private List<ParticleSystem> _pSystems;

    private void Awake()
    {
        _pSystems = new List<ParticleSystem>();
    }

    private void Start()
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
            beam.SetActive(true);
        }

        foreach (ParticleSystem pSystem in _pSystems)
        {
            pSystem.enableEmission = true;
        }

        foreach (GameObject obj in ToAffect)
        {
            switch (action)
            {
                case Action.ToFreese: obj.SetActive(true);
                    break;
                case Action.ToMelt: obj.SetActive(false);
                    break; 
            }
        }


        MuzzleLight.SetActive(true);
        ImpactLight.SetActive(true);
        if (Sound)
        {
            if (true)
            {
                Sound.Play();
            }
            else
            {
                Sound.Stop();
            }
        }
    }

    private void Update()
    {
        foreach (var receiver in Beams.SelectMany(beam => beam.GetComponent<LightningEmitter>().lightningReceivers))
        {
            receiver.transform.position = TargetTransform.position;
        }

        ImpactLight.transform.position = TargetTransform.position; 
    }

    public override void Deactivate()
    {
        foreach (var beam in Beams)
        {
            beam.SetActive(false);
        }

        foreach (ParticleSystem pSystem in _pSystems)
        {
            pSystem.enableEmission = false;
        }

        MuzzleLight.SetActive(false);
        ImpactLight.SetActive(false);
        Sound.Stop();
    }
}
