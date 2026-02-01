using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ParticleCinematic 
{
    [SerializeField] private ParticleSystem _particleSystem;
    [SerializeField] private float _duration;

    public ParticleSystem ParticleSystem => _particleSystem;
    public float Duration => _duration;
}
