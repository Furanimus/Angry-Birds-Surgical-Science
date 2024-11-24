using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Color = System.Drawing.Color;

public class DrawSling : MonoBehaviour
{
    public Transform rightAnchor;
    public Transform leftAnchor;
    private SlingManager _slingManager;
    private LineRenderer _lineRenderer;
    private Vector3 _projectilePosition;
    
    // Start is called before the first frame update
    void Start()
    {
        _slingManager = GetComponent<SlingManager>();
        _slingManager.ProjectileLoaded += ProjectileLoaded;
        _slingManager.ProjectileUnloaded += ProjectileUnloaded;
        _lineRenderer = GetComponent<LineRenderer>();
        ResetPositions();
    }

    private void ProjectileUnloaded()
    {
        _projectilePosition = Vector3.zero;
        ResetPositions();
    }

    private void ResetPositions()
    {
        _lineRenderer.positionCount = 2;
        _lineRenderer.SetPosition(0,rightAnchor.position);
        _lineRenderer.SetPosition(1,leftAnchor.position);
    }

    private void Update()
    {
        if (_lineRenderer.positionCount == 3)
        {
            _projectilePosition = _slingManager.LoadedProjectile.transform.position;
            _lineRenderer.SetPosition(1,_projectilePosition);
        }
    }

    private void ProjectileLoaded(Vector3 position)
    {
        _projectilePosition = position;
        if (_lineRenderer.positionCount < 3)
        {
            _lineRenderer.positionCount = 3;
        }
        _lineRenderer.SetPosition(0, leftAnchor.position);
        _lineRenderer.SetPosition(1, _projectilePosition);
        _lineRenderer.SetPosition(2, rightAnchor.position);
    }
}
