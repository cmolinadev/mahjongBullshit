using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Udaeta : MonoBehaviour
{
    [SerializeField] List<int> yus = new List<int>();

    
    public List<RiData> GetRisData(int number)
    {
        List<RiData> list = new List<RiData>();
        for (int i = 0; i < number; i++)
        {
            list.Add(GetRandomData());
        }

        return list;
    }

    private RiData GetRandomData()
    {
        var yuRandom = Random.Range(0, yus.Count);
        var yu = yus[yuRandom];

        var semillaRandom = Random.Range(0, 2);
        var semilla = semillaRandom switch
        {
            0 => Semilla.Tri,
            1 => Semilla.Chi,
            2 => Semilla.Mu,
            _ => Semilla.Tri
        };
        return new RiData(yu, semilla);
    }
}
