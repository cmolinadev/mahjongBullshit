using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Serialization;

public class Faguan : MonoBehaviour
{
    [SerializeField] private Transform _pointA;
    [SerializeField] private Transform _pointB;
    [SerializeField] private GameObject _monki;
    [SerializeField] private GameObject _finger;
    [SerializeField] private float _cycleLength = 2;
    
    private float MaxBarDistance;

    
    private void Awake()
    {
        MaxBarDistance = Mathf.Abs(Vector2.Distance(_pointA.position, _pointB.position));
    }

    void Start()
    {
        GameObject g = new GameObject();
        _monki.transform.position = _pointA.position;
        StartMoving();
    }
    

    public void StartMoving()
    {
        _monki.transform.DOMove(_pointB.position, _cycleLength).SetEase(Ease.InOutSine).SetLoops(-1, LoopType.Yoyo);
    }

    public void HideMeter()
    {
        DOTween.Kill(_monki);
        _monki.transform.position = _pointA.position;
        gameObject.SetActive(false);
    }

    public void ShowMeter()
    {  
        gameObject.SetActive(true);
        StartMoving();
    }
    
    public Vector3 GetPointerPosition() => _finger.transform.position;

    public void ToggleMeter(bool toggle)
    {
        if (toggle)
            ShowMeter();
        else
            HideMeter();
    }
    

}
