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
    [SerializeField] private ChiBar _chiBar;
    [SerializeField] private Dialogo _dialogo;
    private bool _inProgress = false;
    
    public Dialogo Dialogo => _dialogo;
    public bool Finished =>  !_inProgress;
    public Mano Mano => _mano;
    public BrisaManager BrisaManager => _brisaManager;

    public void SetChiBar(int chi)
    {
        _chiBar.SetChi(chi);
    }

    public void InitializeChiBar(int maxChi)
    {
        _chiBar.Initialize(maxChi);
    }
    
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

    public void OnBrisaFinished(bool finishViento)
    {
        if (finishViento)
        {
            FinishViento();
            return;
        }
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
        _dialogo.ShowRandomVientoText();
    }

    public void FinishViento()
    {
        _brisaManager.Cleanup();
        _inProgress = false;
    }
}
