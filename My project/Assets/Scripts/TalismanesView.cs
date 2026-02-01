using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalismanesView : MonoBehaviour
{
    [SerializeField] private List<GameObject> _talismanes;
    [SerializeField] private ParticleSystem _particles;

    public void SetCount(int count)
    {
        var counting = count;
        foreach (var talsiman in _talismanes)
        {
            var wasActive = talsiman.activeSelf;
            var toActive = counting > 0;
            if (wasActive && !toActive)
                Instantiate(_particles, talsiman.transform.position, Quaternion.identity);

            talsiman.SetActive(toActive);
            counting--;
        }
    }
}
