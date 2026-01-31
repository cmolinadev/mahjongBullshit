using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Viento : MonoBehaviour
{
    public int _numeroBrisas = 3;
    private int _remainigBrisas;
    [SerializeField] BrisaManager _brisaManager;
    private bool _inProgress = false;
    
    public bool Finished =>  !_inProgress;

    private void Update()
    {
        if (!_inProgress)
            return;
        
        GetPlayerInput();
    }
    

    void GetPlayerInput()
    {
        if (!CanDrop)
            return;

        if (Input.GetMouseButtonDown(0))
        {
            if (_brisaManager.isHolding)
                _brisaManager.SpawnRi();
        }
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
        _inProgress = true;
    }

    public void FinishViento()
    {
        _brisaManager.Cleanup();
        _inProgress = false;
    }
}
