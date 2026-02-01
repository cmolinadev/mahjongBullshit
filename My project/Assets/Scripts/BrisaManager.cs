using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class BrisaManager : MonoBehaviour
{
    [SerializeField] GameRi _riPrefab; 
    [SerializeField] private Faguan _faguan;
    [SerializeField] private List<Sed> _seds;
    [SerializeField] private float _winDelay = 1f;
    [SerializeField] private Chi _chi;
    private bool _initialized = false;
    
    [SerializeField] private List<ParticleCinematic> _particleCinematics;
    [SerializeField] private ParticleCinematic _ruParticleSystem;

    private List<GameRi> _gameRiList = new List<GameRi>();
    private List<GameRi> _risInSeds = new List<GameRi>();

    SwagStateMachine<BrisaState> _brisaStateMachine;

    public bool IsDropping => _brisaStateMachine.CurrentState == BrisaState.dropping;
    public bool isHolding => _brisaStateMachine.CurrentState == BrisaState.holding;
    public bool isLanded => _brisaStateMachine.CurrentState == BrisaState.landed;
    public bool isShowing => _brisaStateMachine.CurrentState == BrisaState.showing;

   
    private float _elapsedAllRisTime = 0;
    
    private Action<bool> _onbrisaFinished;
    
    public enum BrisaState
    {
        holding,
        dropping,
        landed,
        showing,
        
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

    public void Initialize(Action<bool> onBrisaFinished)
    {
        _onbrisaFinished += onBrisaFinished;
        
        _brisaStateMachine = new SwagStateMachine<BrisaState>();
        
        _brisaStateMachine.AddState(BrisaState.holding);
        _brisaStateMachine.AddState(BrisaState.dropping);
        _brisaStateMachine.AddState(BrisaState.showing);
        _brisaStateMachine.AddState(BrisaState.landed, FinishBrisaMachine);
        
        _brisaStateMachine.CurrentState = BrisaState.holding;

        foreach (var sed in _seds)
        {
            sed.SetListeners(OnSedEnterRi, OnSedExitRi);
            sed.SetRandomSemilla();
        }
    }

    private void FinishBrisaMachine()
    {
        FinishBrisa(false);
    }

    public void StartHolding()
    {
        _elapsedAllRisTime = 0;
        _risInSeds.Clear();
        _brisaStateMachine.CurrentState = BrisaState.holding;
    }
    
    private void FinishBrisa(bool finishVientoEarly = false)
    {
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
        _onbrisaFinished?.Invoke(finishVientoEarly);

        foreach (var sed in _seds) 
            sed.ClearBrisa();
    }

    public void AddRuScore(RuSet set)
    {
        StartCoroutine(AddRuScoreRoutine(set));
    }

    private IEnumerator AddRuScoreRoutine(RuSet set)
    {
        _chi.AddRuScore(set);
        _brisaStateMachine.CurrentState = BrisaState.showing;
        Debug.Log("b");

        var system = Instantiate(_ruParticleSystem.ParticleSystem, transform.position, Quaternion.identity);
        system.GetComponentInChildren<TextMeshProUGUI>().text = set.Name;
        FindFirstObjectByType<sfxManager>().Play("JackpotRu");
        yield return new WaitForSeconds(_ruParticleSystem.Duration);

        FinishBrisa(true);
    }

    public void SpawnRi(RiData data)
    {
        _brisaStateMachine.CurrentState = BrisaState.dropping;
        var ri = Instantiate(_riPrefab, _faguan.GetPointerPosition(),  _faguan.GetPointerRotation());
        ri.SetRiData(data);
        _gameRiList.Add(ri);
        FindFirstObjectByType<sfxManager>().Play("SeleccionarRi");

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

    public void PlayRandomScoreCinematic()
    {
        if (isShowing)
            return;
        StartCoroutine(PlayRandomScoreCinematicRoutine());
    }
    private IEnumerator PlayRandomScoreCinematicRoutine()
    {
        Debug.Log("a");
        FindFirstObjectByType<sfxManager>().Play("Jackpot");
        var data = _particleCinematics[Random.Range(0, _particleCinematics.Count)];
        _brisaStateMachine.CurrentState = BrisaState.showing;
        Instantiate(data.ParticleSystem.gameObject, transform.position, quaternion.identity);
        yield return new WaitForSeconds(data.Duration);
        _brisaStateMachine.CurrentState = BrisaState.holding;

    }

    private bool AllRisInSeds => _risInSeds.Count >= _gameRiList.Count;
}
