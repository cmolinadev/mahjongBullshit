using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;

public class HandRi : MonoBehaviour
{
    [SerializeField][ReadOnly] private RiData _riData;
    [SerializeField] private SpriteRenderer _visuals;
    [SerializeField] private RiVisualConfig _riVisuals;

    private Sequence _showSequence;
    Viento _viento;
    
    public RiData RiData => _riData;


    public void Initialize(Viento viento)
    {
        _viento = viento;
        SetShowAnimation();
        Hide();
    }
    public void SetRiData(RiData riData)
    {
        _riData = riData;
        SetVisuals(riData);
    }

    private void SetVisuals(RiData riData)
    {
        _visuals.sprite = _riVisuals.GetRiSprite(riData.Semilla, riData.Yu);
    }

    private void OnMouseOver()
    {
        if (_viento.BrisaManager.isShowing)
            return;
        
        if (IsHidden)
            return;
        
        if (Input.GetMouseButtonDown(0))
        {
           if (_viento.SpawnRi(_riData)) 
               Hide();
        }

        if (!Input.GetMouseButtonDown(1)) return;
        if (_viento.Mano.IntentarMirada())
            MiradaDeFortuna();
    }

    private void MiradaDeFortuna()
    {
        var data = _viento.Mano.Udaeta.GetRisData(1);
        SetRiData(data.First());
    }

    private bool IsHidden => !_visuals.gameObject.activeSelf;
    

    public void Show()
    {
        _visuals.gameObject.SetActive(true);
        _showSequence.Play();
    }
    public void Hide()
    {
        _visuals.gameObject.SetActive(false);
    }

    void SetShowAnimation()
    {
        var scale = _visuals.transform.localScale;
        _visuals.transform.localScale = Vector3.zero;

        Sequence seq = DOTween.Sequence();

        seq.Append(_visuals.transform.DOScale(scale, 0.25f).SetEase(Ease.OutQuad));
        seq.Append(_visuals.transform.DOScale(scale, 0.15f).SetEase(Ease.OutBounce));
        _showSequence = seq;
    }
}
