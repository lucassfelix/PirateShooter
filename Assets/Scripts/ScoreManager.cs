using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    private TextMeshProUGUI _scoreValue;
    private int _score;
    
    // Start is called before the first frame update
    void Start()
    {
        _score = 0;
        _scoreValue = GetComponent<TextMeshProUGUI>();
    }

    public void AddScore()
    {
        _score += 1;
        _scoreValue.text = _score.ToString();
    }
    
    
    
}
