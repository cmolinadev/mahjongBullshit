using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GOLPEENLAMESA : MonoBehaviour
{
   [SerializeField] private RuLibrary _ruLibrary;
   [SerializeField] private Mano _mano;
   [SerializeField] private Viento _viento;
   
   private void OnMouseOver()
   {
      if (Input.GetMouseButtonDown(0) && CanHit)
      {
        GOLPEAR();
      }
   }

   public bool CanHit => !_viento.Finished;

   private void GOLPEAR()
   {
      var riDatas = _mano.HandRis
         .Select(handRi => handRi.RiData).ToList();
      
      CheckForRus(riDatas);
   }

   private void CheckForRus(List<RiData> risInHand)
   {
      var effect = _ruLibrary.TryGetRu(risInHand);
      if (effect == RuSet.RuEffect.None)
         GotNothing();
      else
      {
         Debug.Log("VICTORIA SUPREMA: "+effect);
      }
   }

   private void GotNothing()
   {
      Debug.Log("derrota ancestral");
   }
}
