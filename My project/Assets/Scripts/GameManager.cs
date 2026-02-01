using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private List<Viento> _vientos;
    [SerializeField] private Chi _chi;
    Viento _currentViento;
    private bool gameStarted = false;
    bool gameOver = false;
    public Viento CurrentViento => _currentViento;
    
    void Update()
    {
        if (gameOver)
            return;

        if (!gameStarted)
        {
            StartGame();
            return;
        }
        
        TickGameProgress();
    }

    private void TickGameProgress()
    {
        if (CurrentVientoInProgress()) return;

        var win = _chi.IsVientoWon(_currentViento);
        FinishCurrentViento();

        if (!win)
        {
            Lose();
            return;
        }
        if (_vientos.Count == 0)
        {
            Win();
            return;
        }
        
        ServeNextViento();
    }
    
    private void FinishCurrentViento()
    {
        _currentViento.FinishViento();
        _currentViento.gameObject.SetActive(false);
        _vientos.RemoveAt(0);
    }

    private void StartGame()
    {
        ServeNextViento();
        gameStarted = true;
    }
    
    private bool CurrentVientoInProgress() => !_currentViento.Finished;

    private void ServeNextViento()
    {
        _currentViento = _vientos[0];
        _currentViento.gameObject.SetActive(true);
        _currentViento.StartViento();
        _chi.InitializeChiBar();
        _chi.UpdateChiBar();
    }

    private void Win()
    {
        Debug.Log("yujus");
        gameOver = true;
    }
    
    private void Lose()
    {
        Debug.Log("la peldiste");
        gameOver = true;
    }

    public void PlayUdaetaVisuals(UdaetaView.UdaetaState state)
    {
        
    }

}
