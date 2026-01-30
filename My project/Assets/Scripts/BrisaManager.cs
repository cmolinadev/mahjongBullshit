using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BrisaManager : MonoBehaviour
{
    [SerializeField] GameRi _riPrefab; //hacer tipo luego o algo
    [SerializeField] private Faguan _faguan;
    [SerializeField] private List<Sed> _seds;
    private bool _initialized = false;

    private List<GameRi> _gameRiList = new List<GameRi>();
    private List<GameRi> _risInSeds = new List<GameRi>();

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

        foreach (var sed in _seds)
        {
            sed.SetListeners(OnSedEnterRi, OnSedExitRi);
        }
    }
    
    public void SpawnRi()
    {
        _brisaStateMachine.CurrentState = BrisaState.dropping;
        var ri = Instantiate(_riPrefab, _faguan.GetPointerPosition(), Quaternion.identity);
        _gameRiList.Add(ri);
    }


    private void OnSedEnterRi(GameRi ri)
    {
        _risInSeds.Add(ri);
    }

    private void OnSedExitRi(GameRi ri)
    {
        _risInSeds.Remove(ri);
    }
    
    public void Cleanup()
    {
        foreach (var sed in _seds)
        {
            sed.RemoveListeners(OnSedEnterRi, OnSedExitRi);
        }
    }
 
}
