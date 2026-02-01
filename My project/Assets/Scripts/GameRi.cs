using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class GameRi : MonoBehaviour
{
    [SerializeField][ReadOnly] private RiData _riData;
    [SerializeField] RiVisualConfig _riVisualConfig;
    [SerializeField] SpriteRenderer _SpriteRenderer;
    public RiData RiData => _riData;

    public void SetRiData(RiData riData)
    {
        _riData = riData;
        SetVisuals(riData);
    }

    private void SetVisuals(RiData riData)
    {
        _SpriteRenderer.sprite = _riVisualConfig.GetRiSprite(riData.Semilla, riData.Yu);
    }

    private void OnCollisionEnter(Collision other)
    {
        FindFirstObjectByType<sfxManager>().Play("RiChoca");

    }
}
