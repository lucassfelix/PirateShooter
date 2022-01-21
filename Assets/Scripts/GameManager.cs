using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    [SerializeField] private GameObject scoreReference;
    [SerializeField] private TextMeshProUGUI scoreValueReference;
    [SerializeField] private GameObject endGameScreenReference;
    [SerializeField] private TextMeshProUGUI endgameScoreReference;
    
    
    private float _gameSessionTime;

    private void Start()
    {
        _gameSessionTime = PlayerPrefs.GetFloat("GameSession");
        Invoke(nameof(EndGame),_gameSessionTime * 60);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

    public void PlayAgain()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void EndGame()
    {
        scoreReference.SetActive(false);
        endGameScreenReference.SetActive(true);
        endgameScoreReference.text = scoreValueReference.text;
    }
}
