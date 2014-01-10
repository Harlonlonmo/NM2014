﻿using System.Collections.Generic;
using System.Linq;
using System.Threading;
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
    public Camera camera; 
    public Action action;
    public float DefreezeSpeed = 0.08f;
    public float LifeTimeActivated = 3;
    public GameObject HeatEffectLight;
    public float DeHeatSpeed = 10; 

    public GameObject MuzzleLight;
    public GameObject ImpactLight;

    public Transform TargetTransform;


    // Private shit
    private List<ParticleSystem> _pSystems;
    private FrostEffect _frost;
    private bool _frostEnabled;
    private float _timer = 0;

    private Light _heatEffectLight;
    private LensFlare _heatEffectFlare;
    private bool _heatEnabled;

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

        _frost = camera.GetComponent<FrostEffect>();

        if (action != Action.ToMelt) return;
        _heatEffectLight = HeatEffectLight.GetComponent<Light>();
        _heatEffectFlare = HeatEffectLight.GetComponent<LensFlare>();
        HeatEffectLight.SetActive(false);
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
                    _frost.FrostAmount = 1;
                    _frostEnabled = true; 
                    break;
                case Action.ToMelt: obj.SetActive(false);
                    HeatEffectLight.SetActive(true);
                    _heatEnabled = true;
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

        _timer = 0; 

    }

    private void Update()
    {
        foreach (var receiver in Beams.SelectMany(beam => beam.GetComponent<LightningEmitter>().lightningReceivers))
        {
            receiver.transform.position = TargetTransform.position;
        }

        ImpactLight.transform.position = TargetTransform.position;

        _timer += Time.deltaTime;
        if (_timer >= LifeTimeActivated)
        {
            Deactivate();
        }

        if (_heatEnabled)
        {
            if (_heatEffectLight.intensity > 0)
            {
                _heatEffectLight.intensity -= Time.deltaTime*DeHeatSpeed;
                _heatEffectFlare.brightness -= Time.deltaTime*DeHeatSpeed;
            }
            else _heatEnabled = false; 
        }


        if(!_frostEnabled) return;
        if (_frost.FrostAmount > 0) _frost.FrostAmount -= Time.deltaTime*DefreezeSpeed;
        else _frostEnabled = false; 
            
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
