using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI ScoreText;
    [SerializeField] private ScoreManager ScoreManager;
    
    public void UpdateScoreText(int score)
    {
        ScoreText.text = $"Score: {score}";
    }
    
    private void Start()
    {
        ScoreText.text = "Score: 0";
    }

    private void Awake()
    {
        ScoreManager.ScoreChanged += UpdateScoreText;
    }
}
