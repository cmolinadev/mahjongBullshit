using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class Dialogo : MonoBehaviour
{
   [SerializeField] private writeText _writeText;
   [FormerlySerializedAs("_dialogues")] [SerializeField] private List<string> _vientoDialogues;
   [SerializeField] private List<string> _onFailureDialogues;
   [SerializeField] private List<string> _refranesString;

   public void ShowRandomVientoText()
   {
      FindFirstObjectByType<sfxManager>().Play(GetRandomRefranString());
      _writeText.changeText(_vientoDialogues[Random.Range(0, _vientoDialogues.Count)]);
   }
   
   public void ShowRandomFailureText()
   {
      FindFirstObjectByType<sfxManager>().Play(GetRandomRefranString());
      _writeText.changeText(_onFailureDialogues[Random.Range(0, _onFailureDialogues.Count)]);
   }
   
   private string GetRandomRefranString() => _refranesString[Random.Range(0, _refranesString.Count)];

}
