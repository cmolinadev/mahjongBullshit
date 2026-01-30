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
        if (!IsNotDropping)
            return;

        if (Input.GetMouseButtonDown(0))
        {
            _brisaManager.SpawnRi();
        }
    }

    public void OnBrisaFinished()
    {
        _remainigBrisas--;
        
    }

    public bool IsNotDropping => !_brisaManager.IsDropping;

    public void StartViento()
    {
        Debug.Log("startViento");
        _brisaManager.Initialize();
        _inProgress = true;
    }

    public void FinishViento()
    {
        _brisaManager.Cleanup();
        _inProgress = false;
    }
}
