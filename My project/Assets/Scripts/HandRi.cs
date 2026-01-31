using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class HandRi : MonoBehaviour
{
    [SerializeField][ReadOnly] private RiData _riData;
    [SerializeField] private GameObject _visuals;

    private bool _handlingEnabled;
    
    public RiData RiData => _riData;


    public void Initialize()
    {
        Hide();
    }
    public void SetRiData(RiData riData)
    {
        _riData = riData;
        SetVisuals(riData);
    }

    private void SetVisuals(RiData riData)
    {
      
    }

    public void ToggleHandling(bool toggle)
    {
        _handlingEnabled = true;
    }

    public void Show()
    {
        _visuals.SetActive(true);
    }
    public void Hide()
    {
        _visuals.SetActive(false);
    }
}
