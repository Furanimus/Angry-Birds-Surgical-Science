using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Transform slingProjectileSource;
    private Vector3 _slingSourcePosition;
    private BoxCollider _slingProjectileSourceCollider;
    public Camera mainCamera;
    private Vector3 _mouseScreenPosition;
    [SerializeField] private float maxDragDistance = 4f;
    [SerializeField] private float yOffset = 4f;
    private bool _isDragging;
    [SerializeField] private float zOffset = 1f; 
    public Transform testProjectile;
    
    private Vector3 _mouseWorldPosition;
    private float _mouseX;
    private float _mouseY;

    private void Start()
    {
        mainCamera = Camera.main;
        _slingProjectileSourceCollider = slingProjectileSource.GetComponent<BoxCollider>();
        _slingSourcePosition = slingProjectileSource.position;
    }

    void Update()
    {
        GetMouseInfo();
        if (Input.GetMouseButtonDown(1))
        { // switch projectile
            Debug.Log("Right Click");
        }
        
        if (Input.GetMouseButtonDown(0))
        {
            CheckSlingDragging();
            if (_isDragging)
            {
                Cursor.visible = false;
            }
        }
        if (Input.GetMouseButton(0) && _isDragging) // Windup + simulate
        {
            _mouseScreenPosition.z = _slingSourcePosition.z - mainCamera.transform.position.z - zOffset;
            _mouseWorldPosition = mainCamera.ScreenToWorldPoint(_mouseScreenPosition);
            Vector3 slingOffset = _mouseWorldPosition - _slingSourcePosition;

            if (slingOffset.magnitude > maxDragDistance)
            {
                _mouseWorldPosition = _slingSourcePosition + slingOffset.normalized * maxDragDistance;
            }
            
            if (_mouseWorldPosition.y > _slingSourcePosition.y)
            {
                _mouseWorldPosition.y = _slingSourcePosition.y;
                _mouseWorldPosition.z = _slingSourcePosition.z - zOffset;
            }
            
            testProjectile.position = _mouseWorldPosition;
            
            Debug.Log(slingOffset.magnitude);   
            // SimulateShot(); //FixedUpdate
        }

        if (Input.GetMouseButtonUp(0)) // Shoot
        {
            Cursor.visible = true;
            _isDragging = false;
            _slingProjectileSourceCollider.enabled = true;
        }
    }

    private void SimulateShot()
    {
        Debug.Log("Simulating");
    }

    private void CheckSlingDragging()
    {
        Ray ray = mainCamera.ScreenPointToRay(_mouseScreenPosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            if (hit.transform == slingProjectileSource)
            {
                _isDragging = true;
                _slingProjectileSourceCollider.enabled = false;
            }
        }
    }
    
    private void GetMouseInfo()
    {
        _mouseScreenPosition = Input.mousePosition;
    }
}