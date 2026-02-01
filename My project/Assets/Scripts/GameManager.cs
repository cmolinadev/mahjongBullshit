using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private List<Viento> _vientos;
    [SerializeField] private Chi _chi;
    [SerializeField] GameObject _menuScreen;
    [SerializeField] GameObject _winScreen;
    [SerializeField] GameObject _loseScreen;
    [SerializeField] private UdaetaView _udaetaView;

    Viento _currentViento;
    private bool inMenu = true;
    private bool gameStarted = false;
    bool gameOver = false;
    public Viento CurrentViento => _currentViento;

    private void Start()
    {
        _menuScreen.SetActive(true);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && gameOver)
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        
        if (Input.GetMouseButtonDown(0))
            FindFirstObjectByType<sfxManager>().Play("Click");

        
        if (inMenu)
        {
            if (Input.GetMouseButtonDown(0))
            {
                _menuScreen.SetActive(false);
                inMenu = false;
            }
            return;
        }
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
        FindFirstObjectByType<sfxManager>().Play("GongStart");
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
        _winScreen.SetActive(true);
        _udaetaView.PlayEmote(UdaetaView.UdaetaState.Clap);
    }
    
    private void Lose()
    {
        Debug.Log("la peldiste");
        gameOver = true;
        _loseScreen.SetActive(true);
        _udaetaView.PlayEmote(UdaetaView.UdaetaState.Wow);

    }

}
