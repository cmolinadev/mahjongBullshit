using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;

public class GOLPEENLAMESA : MonoBehaviour
{
   [FormerlySerializedAs("_ruLibrary")] [SerializeField] private RuRegistry ruRegistry;
   [SerializeField] private Mano _mano;
   [SerializeField] private Viento _viento;

   [SerializeField] private Animator _animator;


   private void OnMouseOver()
   {
      if (Input.GetMouseButtonDown(0) && CanHit)
      {
        GOLPEAR();
        _animator.SetTrigger("Pulsar");
      }
   }
   

   public bool CanHit => !_viento.Finished && !_viento.BrisaManager.isShowing;

   private void GOLPEAR()
   {
      var riDatas = _mano.HandRis
         .Select(handRi => handRi.RiData).ToList();
      
      CheckForRus(riDatas);
   }

   private void CheckForRus(List<RiData> risInHand)
   {
      var effect = ruRegistry.TryGetRu(risInHand);
      if (effect == RuSet.RuEffect.None)
         GotNothing();
      else
      {
         Debug.Log("effect: "+effect);
         AddPoints(effect); //bullshit comparison but here we are
      }
   }

   private void AddPoints(RuSet.RuEffect effect)
   {
      RuSet set = ruRegistry.GetRuByType(effect);
      if (set != null)
      {
         _viento.BrisaManager.AddRuScore(set);
      }
   }

   private void GotNothing()
   {
      Debug.Log("derrota ancestral");
   }
}
