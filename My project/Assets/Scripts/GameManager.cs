using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private List<Viento> _vientos;
    Viento _currentViento;
    bool gameOver = false;
    
    void Update()
    {
        if (gameOver)
            return;

        TickGameProgress();
    }

    private void TickGameProgress()
    {
        TryStartWin();
        
        if (CurrentVientoInProgress()) return;
        
        _vientos.RemoveAt(0);

        if (_vientos.Count == 0)
        {
            Win();
            return;
        }
        
        ServeNextViento();
    }

    private bool CurrentVientoInProgress()
    {
        return !_currentViento.Finished;
    }

    private void TryStartWin()
    {
        if (_currentViento == null)
            _currentViento = _vientos[0];
    }

    private void ServeNextViento()
    {
        _currentViento = _vientos[0];
    }

    private void Win()
    {
        Debug.Log("yujus");
        gameOver = true;
    }
}
