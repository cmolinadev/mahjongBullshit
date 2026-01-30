using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrisaManager : MonoBehaviour
{
    [SerializeField] GameObject _riPrefab; //hacer tipo luego o algo
    [SerializeField] private Faguan _faguan;
    private bool _initialized = false;
    

    public void SpawnRi()
    {
        Instantiate(_riPrefab, _faguan.GetPointerPosition(), Quaternion.identity);
    }


 
}
