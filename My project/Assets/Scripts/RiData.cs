
using System;
using UnityEngine;

[Serializable]
public class RiData
{
    [SerializeField] private float _yu;
    [SerializeField] private Semilla _semilla;

    public float Yu => _yu;
    public Semilla Semilla => _semilla;
}

public enum Semilla
{
    Chi,
    Tri,
    Mu
}