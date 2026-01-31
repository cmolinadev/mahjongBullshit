using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Viento : MonoBehaviour
{
    public int _numeroBrisas = 3;
    private int _remainigBrisas;
    [SerializeField] BrisaManager _brisaManager;
    [SerializeField] private Mano _mano;
    private bool _inProgress = false;
    
    public bool Finished =>  !_inProgress;
    public Mano Mano => _mano;
    
    
    public bool SpawnRi(RiData data)
    {
        if (!_inProgress)
            return false;
        
        if (!CanDrop)
            return false;

        if (!_brisaManager.isHolding) return false;
        
        _brisaManager.SpawnRi(data);
        return true;

    }

    public void OnBrisaFinished()
    {
        _remainigBrisas--;
        if (_remainigBrisas > 0)
            _brisaManager.StartHolding();
        else
            FinishViento();
    }

    public bool CanDrop => _brisaManager.isHolding;

    public void StartViento()
    {
        Debug.Log("startViento");
        _remainigBrisas = _numeroBrisas;
        _brisaManager.Initialize(OnBrisaFinished);
        _mano.Initialize(this);
        _mano.PedirRis();
        _inProgress = true;
    }

    public void FinishViento()
    {
        _brisaManager.Cleanup();
        _inProgress = false;
    }
}
