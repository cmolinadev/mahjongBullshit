using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Mano : MonoBehaviour
{
    [FormerlySerializedAs("_holdingPoints")] [SerializeField] private List<HandRi> _handRis = new List<HandRi>();
    [SerializeField] private Udaeta _udaeta;

    public void Initialize(Viento viento)
    {
        foreach (var handRi in _handRis)
        {
            handRi.Initialize(viento);
        }
    }

    public void PedirRis()
    {
        StartCoroutine(PedirRisRoutine());
    }
    public IEnumerator PedirRisRoutine()
    {
       var data = _udaeta.GetRisData(_handRis.Count);
       foreach (var handRi in _handRis)
       {
           yield return new WaitForSeconds(0.8f/2f);
           handRi.SetRiData(data[0]);
           data.RemoveAt(0);
           handRi.Show();
       }
    }
    
}
