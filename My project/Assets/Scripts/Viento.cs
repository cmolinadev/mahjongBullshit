using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Viento : MonoBehaviour
{
    public int _numeroBrisas = 3;
    private int _remainigBrisas;
    [SerializeField] BrisaManager _brisaManager;
    private bool _inProgress = true;
    
    public bool Finished =>  !_inProgress;

    private void Update()
    {
        if (!_inProgress)
            return;
        
        GetPlayerInput();
    }
    

    void GetPlayerInput()
    {
        if (!CanGetInput)
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

    public bool CanGetInput => true;
}
