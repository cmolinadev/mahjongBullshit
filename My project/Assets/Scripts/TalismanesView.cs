using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalismanesView : MonoBehaviour
{
    [SerializeField] private List<GameObject> _talismanes;

    public void SetCount(int count)
    {
        var counting = count;
        foreach (var talsiman in _talismanes)
        {
            talsiman.SetActive(counting>0);
            counting--;
        }
    }
}
