using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "RuLibrary", menuName = "ScriptableObjects/Rus/RuLibrary", order = 1)]
public class RuRegistry : ScriptableObject
{
    [SerializeField] List<RuSet> RuSets = new List<RuSet>();

    public RuSet.RuEffect TryGetRu(List<RiData> ri)
    {
        foreach (var set in RuSets)
        {
            var effect = set.CheckIfMatches(ri);
            if (effect != RuSet.RuEffect.None)
                return effect;
        }

        return RuSet.RuEffect.None;
    }

    public RuSet GetRuByType(RuSet.RuEffect effect)
    {
        return RuSets.FirstOrDefault(set => set.Rueffect == effect);
    }
}

[Serializable]
public class RuSet
{
    [SerializeField] private List<RiData> _riData;
    [SerializeField] private RuEffect _ruEffect;
    [SerializeField] private int _scoreBonus = 1000;
    [SerializeField] private string _name;
    
    public RuEffect Rueffect => _ruEffect;
    public int ScoreBonus => _scoreBonus;
    public string Name => _name;
    
    public enum RuEffect
    {
        None,
        CONQUISTA,
        PrimaveraHumilde,
        VientoDelEste,
        CaballoGentil,
        AmanecerBrillante,
        LunasGemelas,
        Tsunami,
        GuerreroPoeta,
        MantisReligiosa,
        CaídaDeLaAraña,
        Muralla,
        Refugio,
        LotoNegro,
        NieblaDorada,
        MilenioDelChi,
        GrullaCharlatana
    }

    public RuEffect CheckIfMatches(List<RiData> externalRiData)
    {
        List<RiData> ownDataThatMatch = new List<RiData>();
        foreach (var data in _riData) //revisar
        {
            foreach (var incomingData in externalRiData.Where(incomingData => incomingData.Yu == data.Yu && 
                                                                              incomingData.Semilla == data.Semilla))
            {
                ownDataThatMatch.Add(incomingData);
                Debug.Log("matches!!"+ownDataThatMatch.Count + data.Semilla+" - "+data.Yu);
                externalRiData.Remove(incomingData);
                break;
            }
        }
        
        return ownDataThatMatch.Count == _riData.Count ? 
            _ruEffect : 
            RuEffect.None;
    }
}
