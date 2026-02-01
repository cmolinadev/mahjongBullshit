using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using Unity.VisualScripting;
using UnityEngine;

public class Chi : MonoBehaviour
{
    [SerializeField] private List<ChiGoal> _chiGoals = new List<ChiGoal>();
    [SerializeField] private GameManager _gameManager;
    [SerializeField] private UdaetaView _udaetaView;
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
            Debug.Log("semilla es igual??? "+data.Semilla + " - "+semillaSed+" - "+(data.Semilla == semillaSed));

            if (sed.Semilla == Semilla.Oni)
            {
                _accountingScore -= (int)_expoScoreCurve.Evaluate(data.Yu);
            } else{
                AnimationCurve curve = data.Semilla == semillaSed ?
                _expoScoreCurve : _linearScoreCurve;
                _accountingScore += (int)curve.Evaluate(data.Yu);
            }
        }
        Debug.Log(_accountingScore);
    }

    public void CalculateScore()
    {
       _currentChi += _accountingScore;
       
       UpdateChiBar();
       SetUdaetaView(_accountingScore);
       _accountingScore = 0;
    }

    private void SetUdaetaView(int accountingScore)
    {
        var state = UdaetaView.UdaetaState.Idle;

        if (accountingScore <= 0)
        {
            state = UdaetaView.UdaetaState.No;
            if (accountingScore <= -400)
                state = UdaetaView.UdaetaState.Wow;
        }
        
        if (accountingScore >= 0)
        {
            state = _accountingScore < 50 ? 
                UdaetaView.UdaetaState.Smoke : 
                UdaetaView.UdaetaState.Clap;
            
            if (accountingScore >= 400)
                state = UdaetaView.UdaetaState.Wow;
        }
       
        _udaetaView.PlayEmote(state);
    }

    public void UpdateChiBar()
    {
        _gameManager.CurrentViento.SetChiBar(_currentChi);
    }

    public void InitializeChiBar()
    {
        _gameManager.CurrentViento.InitializeChiBar(GetTotalMaxChi());
    }

    private int GetTotalMaxChi()
    {
        return _chiGoals.Sum(goal => goal.Goal);
    }

    public void AddRuScore(RuSet set)
    {
        _accountingScore = set.ScoreBonus;
        CalculateScore();
    }
}

[Serializable]
public class ChiGoal
{
    [SerializeField] Viento _viento;
    [SerializeField] int _chiGoal;
    
    public Viento Viento => _viento;
    public int Goal => _chiGoal;
}