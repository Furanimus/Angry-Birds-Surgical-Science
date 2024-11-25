using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class WaypointTarget : Target
{
    [SerializeField] private List<Transform> waypoints;
    private List<Vector3> _worldWaypoints; 
    private int _currentWaypointIndex;
    private Vector3 _nextWaypoint;
    
    // Start is called before the first frame update
    private void Start()
    {
        ConvertWaypointsToWorldPosition();
        _nextWaypoint = _worldWaypoints[0];
    }

    private void ConvertWaypointsToWorldPosition()
    {
        _worldWaypoints = new List<Vector3>();
        foreach (Transform waypoint in waypoints)
        {
            _worldWaypoints.Add(waypoint.position);
        }
    }

    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, _nextWaypoint, velocity * Time.deltaTime);
        
        if (Vector3.Distance(transform.position, _nextWaypoint) <= 0.1f)
        {
            _currentWaypointIndex = (_currentWaypointIndex + 1) % _worldWaypoints.Count;
            _nextWaypoint = _worldWaypoints[_currentWaypointIndex];
        }
    }
}
