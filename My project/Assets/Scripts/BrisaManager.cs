using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BrisaManager : MonoBehaviour
{
    [SerializeField] GameRi _riPrefab; 
    [SerializeField] private Faguan _faguan;
    [SerializeField] private List<Sed> _seds;
    [SerializeField] private float _winDelay = 1f;
    [SerializeField] private Chi _chi;
    private bool _initialized = false;

    private List<GameRi> _gameRiList = new List<GameRi>();
    private List<GameRi> _risInSeds = new List<GameRi>();

    SwagStateMachine<BrisaState> _brisaStateMachine;

    public bool IsDropping => _brisaStateMachine.CurrentState == BrisaState.dropping;
    public bool isHolding => _brisaStateMachine.CurrentState == BrisaState.holding;
    public bool isLanded => _brisaStateMachine.CurrentState == BrisaState.landed;

    private float _elapsedAllRisTime = 0;
    
    private Action _onbrisaFinished;
    
    public enum BrisaState
    {
        holding,
        dropping,
        landed,
        
    }

    private void Update()
    {
        TickRedsTime();
    }

    private void TickRedsTime()
    {
        if (_brisaStateMachine == null)
            return;
        
        if ( _brisaStateMachine.CurrentState != BrisaState.dropping)
            return;
        
        if (!AllRisInSeds)
            _elapsedAllRisTime = 0;
        
        _elapsedAllRisTime += Time.deltaTime;
        if (_elapsedAllRisTime >= _winDelay)
            _brisaStateMachine.CurrentState = BrisaState.landed;
    }

    public void Initialize(Action onBrisaFinished)
    {
        _onbrisaFinished += onBrisaFinished;
        
        _brisaStateMachine = new SwagStateMachine<BrisaState>();
        
        _brisaStateMachine.AddState(BrisaState.holding);
        _brisaStateMachine.AddState(BrisaState.dropping);
        _brisaStateMachine.AddState(BrisaState.landed, FinishBrisa);
        
        _brisaStateMachine.CurrentState = BrisaState.holding;

        foreach (var sed in _seds)
        {
            sed.SetListeners(OnSedEnterRi, OnSedExitRi);
            sed.SetRandomSemilla();
        }
    }

    public void StartHolding()
    {
        _elapsedAllRisTime = 0;
        _risInSeds.Clear();
        _brisaStateMachine.CurrentState = BrisaState.holding;
    }
    
    private void FinishBrisa()
    {
        //puntos caballos y cosas
        var newList = new List<GameRi>(_gameRiList);

        foreach (var sed in _seds)
        {
            _chi.AddResult(sed, sed.GetRiData());
        }

        _chi.CalculateScore();
        
        foreach (var ris in newList)
        {
            _gameRiList.Remove(ris);
            Destroy(ris.gameObject);
        }
        
        newList.Clear();
        _onbrisaFinished?.Invoke();

        foreach (var sed in _seds) 
            sed.ClearBrisa();
    }

    public void SpawnRi(RiData data)
    {
        _brisaStateMachine.CurrentState = BrisaState.dropping;
        var ri = Instantiate(_riPrefab, _faguan.GetPointerPosition(),  _faguan.GetPointerRotation());
        ri.SetRiData(data);
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
        _onbrisaFinished -= _onbrisaFinished;
    }

    private bool AllRisInSeds => _risInSeds.Count >= _gameRiList.Count;
}
