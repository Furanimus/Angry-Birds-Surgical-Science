using UnityEngine;
using UnityEngine.Serialization;

public class PlayerController : MonoBehaviour
{
    public Transform slingProjectileSource;
    private Vector3 _slingSourcePosition;
    public Camera mainCamera;
    private Vector3 _mouseScreenPosition;
    private bool _isDragging;
    private Vector3 _mouseWorldPosition;
    
    //Sling Manager
    public SlingManager slingManager;

    private void Start()
    {
        mainCamera = Camera.main;
        _slingSourcePosition = slingProjectileSource.position;
    }

    void Update()
    {
        GetMouseInfo();
        if (Input.GetMouseButtonDown(1))
        { // switch projectile
            slingManager.SwitchProjectile();
            Debug.Log("Right Click");
        }
        
        if (Input.GetMouseButtonDown(0))
        {
            CheckSlingDragging();
            if (_isDragging)
            {
                Cursor.visible = false;
                slingManager.OnProjectileLoaded();
            }
        }
        if (Input.GetMouseButton(0) && _isDragging) // Windup + simulate
        {
            _mouseScreenPosition.z = slingManager.SlingSourcePosition.z - mainCamera.transform.position.z - slingManager.ZOffset;
            _mouseWorldPosition = mainCamera.ScreenToWorldPoint(_mouseScreenPosition);
            slingManager.EnforceSlingConstraints(_mouseWorldPosition); 
            // slingManager.SimulateShot(); //FixedUpdate
        }

        if (Input.GetButtonDown("Cancel"))
        {
            _isDragging = false;
            Cursor.visible = true;
            slingManager.ResetProjectile();
            slingManager.OnProjectileUnloaded();
        }
        
        if (Input.GetMouseButtonUp(0) && _isDragging) // Shoot
        {
            slingManager.Shoot();
            Cursor.visible = true;
            _isDragging = false;
            
            
            // Rigidbody projectileRb = testProjectile.GetComponent<Rigidbody>();
            // projectileRb.isKinematic = false;
            // Vector3 shotDirection = _slingSourcePosition - testProjectile.position;
            // // Debug.Log(shotDirection);
            // projectileRb.AddForce(shotDirection * shotMultiplier, ForceMode.Impulse);
            
            //Reset

        }
    }
    
    private void CheckSlingDragging()
    {
        Ray ray = mainCamera.ScreenPointToRay(_mouseScreenPosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            if (hit.transform == slingProjectileSource)
            {
                _isDragging = true;
            }
        }
    }
    
    private void GetMouseInfo()
    {
        _mouseScreenPosition = Input.mousePosition;
    }
}