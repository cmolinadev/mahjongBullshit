using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;

public class Mano : MonoBehaviour
{
    [FormerlySerializedAs("_holdingPoints")] [SerializeField] private List<HandRi> _handRis = new List<HandRi>();
    [SerializeField] private Udaeta _udaeta;
    [SerializeField] private float _miradasDeFortuna = 3;
    private float _miradasDeFortunaRestantes = 0;
    
    public Udaeta Udaeta => _udaeta;
    
    public void Initialize(Viento viento)
    {
        foreach (var handRi in _handRis)
        {
            handRi.Initialize(viento);
        }
    }
    

    public void PedirRis()
    {
        RecargarMirada();
        StartCoroutine(PedirRisRoutine());
    }
    

    private void RecargarMirada()
    {
        _miradasDeFortunaRestantes = _miradasDeFortuna;
    }

    public bool IntentarMirada()
    {
        if (!(_miradasDeFortunaRestantes > 0)) return false;
        
        _miradasDeFortunaRestantes --;
        return true;
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
