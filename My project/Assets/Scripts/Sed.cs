using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sed : MonoBehaviour
{
    private Action<GameRi> _onEnter;
    private Action<GameRi> _onExit;
    
    private Semilla _semilla;

    public void SetData(Semilla semilla)
    {
        _semilla = semilla;
        SetVisuals(semilla);
    }

    private void SetVisuals(Semilla semilla)
    {
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.GetComponent<GameRi>())
            return;
        
        _onEnter?.Invoke(other.gameObject.GetComponent<GameRi>());
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.GetComponent<GameRi>())
            return;
        
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
}
