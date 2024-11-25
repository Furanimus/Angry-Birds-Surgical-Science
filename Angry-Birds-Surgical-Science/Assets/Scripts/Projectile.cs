using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] protected float velocity = 20f;
    protected Rigidbody _rigidbody;

    // [SerializeField] protected Color color = Color.cyan;

    public Rigidbody Rigidbody => _rigidbody;
    
    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }
}
