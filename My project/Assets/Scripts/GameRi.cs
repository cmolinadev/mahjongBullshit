using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameRi : MonoBehaviour
{
    [SerializeField] private RiData _riData;
    public RiData RiData => _riData;

    public void SetRiData(RiData riData)
    {
        _riData = riData;
        SetVisuals(riData);
    }

    private void SetVisuals(RiData riData)
    {
      
    }
}
