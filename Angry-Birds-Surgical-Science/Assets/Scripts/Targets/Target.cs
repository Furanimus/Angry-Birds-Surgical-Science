using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Target : MonoBehaviour
{
    [SerializeField] protected float velocity = 10f;
    [SerializeField] protected int score = 5;
    [SerializeField] protected ScoreManager _scoreManager;

    private void Awake()
    {
        _scoreManager = FindObjectOfType<ScoreManager>();
    }

    protected void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Projectile"))
        {
            _scoreManager.AddScore(score);
            Destroy(gameObject);
            Destroy(other.gameObject);
        }    
    }
}
