
using System;
using UnityEngine;

[Serializable]
public class RiData
{
    [SerializeField] private float _yu;
    [SerializeField] private Semilla _semilla;

    public RiData(int yu, Semilla semilla)
    {
        _yu = yu; 
        _semilla = semilla;
    }

    public float Yu => _yu;
    public Semilla Semilla => _semilla;
}

public enum Semilla
{
    Chi,
    Tri,
    Mu,
    Dragon,
    Oni,
    Vacio
}