using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawSling : MonoBehaviour
{
    public Transform rightAnchor;
    public Transform leftAnchor;

    private LineRenderer _lineRenderer;
    
    // Start is called before the first frame update
    void Start()
    {
        _lineRenderer = GetComponent<LineRenderer>();
        _lineRenderer.positionCount = 2;
        _lineRenderer.SetPosition(0,rightAnchor.position);
        _lineRenderer.SetPosition(1,leftAnchor.position);
    }
}
