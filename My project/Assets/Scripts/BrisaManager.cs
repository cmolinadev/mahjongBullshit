using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BrisaManager : MonoBehaviour
{
    [SerializeField] GameObject _riPrefab; //hacer tipo luego o algo
    [SerializeField] private Faguan _faguan;
    private bool _initialized = false;

    SwagStateMachine<BrisaState> _brisaStateMachine;

    public bool IsDropping => _brisaStateMachine.CurrentState == BrisaState.dropping;
    public bool isHolding => _brisaStateMachine.CurrentState == BrisaState.holding;
    public bool isLanded => _brisaStateMachine.CurrentState == BrisaState.landed;
    
    public enum BrisaState
    {
        holding,
        dropping,
        landed
    }

    public void Initialize()
    {
        _brisaStateMachine = new SwagStateMachine<BrisaState>();
        
        _brisaStateMachine.AddState(BrisaState.holding);
        _brisaStateMachine.AddState(BrisaState.dropping);
        _brisaStateMachine.AddState(BrisaState.landed);
        
        _brisaStateMachine.CurrentState = BrisaState.holding;
    }
    public void SpawnRi()
    {
        _brisaStateMachine.CurrentState = BrisaState.dropping;
        Instantiate(_riPrefab, _faguan.GetPointerPosition(), Quaternion.identity);
    }


 
}
