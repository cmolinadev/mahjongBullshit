using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chi : MonoBehaviour
{
    [SerializeField] private List<ChiGoal> _chiGoals = new List<ChiGoal>();
    private int _currentChi;
    
    public bool IsVientoWon(Viento currentViento)
    {
        foreach (var goal in _chiGoals)
        {
            if (goal.Viento == currentViento)
                return _currentChi >= goal.Goal;
        }

        return false;
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