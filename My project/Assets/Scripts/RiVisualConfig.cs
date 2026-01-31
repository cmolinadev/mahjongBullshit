using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "RiVisualConfig", menuName = "ScriptableObjects/Visuals/RiVisualConfig", order = 1)]
public class RiVisualConfig : ScriptableObject
{
    [SerializeField] private List<Sprite> _riTris;
    [SerializeField] private List<Sprite> _riChis;
    [SerializeField] private List<Sprite> _riMos;
    

    [SerializeField] private Sprite  _sedTri;
    [SerializeField] private Sprite  _sedChi;
    [SerializeField] private Sprite  _sedMo;

    [SerializeField] private Sprite _dragonSed;
    [SerializeField] private Sprite _oniSed;
    [SerializeField] private Sprite _vacioSed;
    
    
    public Sprite GetRiSprite(Semilla semilla, int yu)
    {
        switch (semilla)
        {
            case Semilla.Chi:
                return _riChis[yu];
            case Semilla.Tri:
                return _riTris[yu];
            case Semilla.Mo:
                return _riMos[yu];
            default:
                return null;
        }
    }

    public Sprite GetSedSprite(Semilla semilla)
    {
        return semilla switch
        {
            Semilla.Chi => _sedChi,
            Semilla.Tri => _sedTri,
            Semilla.Mo => _sedMo,
            _ => null
        };
    }

}
