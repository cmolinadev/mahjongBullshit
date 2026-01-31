using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class Chi : MonoBehaviour
{
    [SerializeField] private List<ChiGoal> _chiGoals = new List<ChiGoal>();
    private int _currentChi;
    
    [BoxGroup("Score")][SerializeField] private AnimationCurve _expoScoreCurve;
    [BoxGroup("Score")][SerializeField] private AnimationCurve _linearScoreCurve;
    
    private int _accountingScore = 0;
    
    public bool IsVientoWon(Viento currentViento)
    {
        foreach (var goal in _chiGoals)
        {
            if (goal.Viento == currentViento)
                return _currentChi >= goal.Goal;
        }

        return false;
    }

    public void AddResult(Sed sed, List<RiData> ri)
    {
        var semillaSed = sed.Semilla;
        foreach (var data in ri)
        {
            AnimationCurve curve = data.Semilla == semillaSed ?
                _expoScoreCurve : _linearScoreCurve;
            _accountingScore += (int)curve.Evaluate(data.Yu);
        }
        Debug.Log(_accountingScore);
    }

    public void CalculateScore()
    {
       _currentChi += _accountingScore;
       _accountingScore = 0;
       Debug.Log("score: "+_currentChi);
    }
}

[Serializable]
public class ChiGoal
{
    [SerializeField] Viento _viento;
    [SerializeField] float _chiGoal;
    
    public Viento Viento => _viento;
    public float Goal => _chiGoal;
}