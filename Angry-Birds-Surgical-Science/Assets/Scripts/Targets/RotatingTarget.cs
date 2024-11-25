using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingTarget : Target
{
    [SerializeField] private Transform rotationTransform;
    private Vector3 rotationPivot;
    
    private enum RotationDirection
    {
        CounterClockwise = 1,
        Clockwise = -1
    }

    [SerializeField] private RotationDirection _direction;
    
    // Start is called before the first frame update
    void Start()
    {
        rotationPivot = rotationTransform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.RotateAround(rotationPivot, Vector3.forward * (int)_direction, velocity * Time.deltaTime);
    }
}
