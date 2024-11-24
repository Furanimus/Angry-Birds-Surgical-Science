using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class IsMouseNearby : MonoBehaviour
{
    private SphereCollider _collider; 
    
    // Start is called before the first frame update
    void Start()
    {
        _collider = GetComponent<SphereCollider>();
    }

    private void OnCollisionEnter(Collision other)
    {
        Debug.Log("Collided");
    }
}
