using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Sed : MonoBehaviour
{
    private Action<GameRi> _onEnter;
    private Action<GameRi> _onExit;
    [SerializeField] private SpriteRenderer _view;
    [SerializeField] private RiVisualConfig _visualConfig;
    private Semilla _semilla;

    private List<RiData> _riDataIn = new List<RiData>();
    private bool _triggeredOni = false;

    public Semilla Semilla => _semilla;
    
    private void SetVisuals(Semilla semilla)
    {
        _view.sprite = _visualConfig.GetSedSprite(semilla);
    }

    public void ClearBrisa()
    {
        _riDataIn.Clear();
        if (_triggeredOni)
            SetOni();

        _triggeredOni = false;
    }

    private void SetOni()
    {
        SetCustomData(Semilla.Oni);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.GetComponent<GameRi>())
            return;
        
        _riDataIn.Add(other.gameObject.GetComponent<GameRi>().RiData);
        _onEnter?.Invoke(other.gameObject.GetComponent<GameRi>());
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.GetComponent<GameRi>())
            return;
        
        _riDataIn.Remove(other.gameObject.GetComponent<GameRi>().RiData);
        _onExit?.Invoke(other.gameObject.GetComponent<GameRi>());
    }
    
    public void SetListeners(Action<GameRi> onEnter, Action<GameRi> onExit)
    {
        _onEnter += onEnter;
        _onExit += onExit;
    }
    
    public void RemoveListeners(Action<GameRi> onEnter, Action<GameRi> onExit)
    {
        _onEnter -= onEnter;
        _onExit -= onExit;
    }

    public void SetRandomSemilla()
    {
        var random = Random.Range(0, 3);
        _semilla = random switch
        {
            0 => Semilla.Chi,
            1 => Semilla.Tri,
            2 => Semilla.Mo,
            _ => Semilla.Vacio
        };
        
        SetVisuals(_semilla);
    }
    
    public void SetCustomData(Semilla semilla)
    {
        _semilla = semilla;
        SetVisuals(semilla);
    }

    public List<RiData> GetRiData()
    {
        foreach (var data in _riDataIn)
        {
            if (data.Semilla != _semilla)
                _triggeredOni = true;
        }
        return _riDataIn;
    }
}
