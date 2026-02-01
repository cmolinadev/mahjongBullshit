using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChiBar : MonoBehaviour
{
    [SerializeField] private Slider _chiSlider;
    [SerializeField] TextMeshProUGUI _chiText;
    private float _maxChi;

    public void Initialize(int maxChi)
    {
        Debug.Log("initalize chi: "+maxChi);
        _maxChi = maxChi;
    }

    public void SetChi(int chi)
    {
        Debug.Log("value = "+chi/_maxChi);
        _chiSlider.value = chi/_maxChi;
        _chiText.text = chi.ToString();
    }
}
