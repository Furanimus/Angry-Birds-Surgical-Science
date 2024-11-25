using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class ExplodingProjectile : Projectile
{
    [SerializeField] private float radius = 1.5f;


    private void Update()
    {
        if (Input.GetButtonDown("ProjectileEffect"))
        {
            _effectCondition = true;
        }
    }

    protected override void DoEffect()
    {
        base.DoEffect();
        // Add explosion logic
        Debug.Log("Boom!");
    }
}