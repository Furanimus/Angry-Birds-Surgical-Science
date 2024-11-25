using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public TextManager TextManager;
    private ScoreManager _instance;
    private int _playerScore;
    public event Action<int> ScoreChanged;

    public ScoreManager Instance
    {
        get => _instance;
        set
        {
            if (_instance == null)
            {
                _instance = gameObject.AddComponent<ScoreManager>();
            }
        }
    }


    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AddScore(int score)
    {
        _playerScore += score;
        OnScoreChanged();
    }

    private void OnScoreChanged()
    {
        ScoreChanged?.Invoke(_playerScore);
    }
}
