
using System;
using UnityEngine;

[Serializable]
public class RiData
{
    [SerializeField] private int _yu;
    [SerializeField] private Semilla _semilla;

    public RiData(int yu, Semilla semilla)
    {
        _yu = yu; 
        _semilla = semilla;
    }

    public int Yu => _yu;
    public Semilla Semilla => _semilla;
}

public enum Semilla
{
    Chi,
    Tri,
    Mo,
    Dragon,
    Oni,
    Vacio
}