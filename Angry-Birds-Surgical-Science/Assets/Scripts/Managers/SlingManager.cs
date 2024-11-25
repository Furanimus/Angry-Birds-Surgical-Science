using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlingManager : MonoBehaviour
{
    [SerializeField] private Projectile loadedProjectile;
    private Dictionary<Projectile, int> _ammo;
    public Transform slingProjectileSource;
    private Vector3 _slingSourcePosition;
    private bool _shouldShoot;
    [SerializeField] private float maxDragDistance = 4f;
    [SerializeField] private float zOffset = 1f;
    [SerializeField] private float shotMultiplier = 5f;
    public event Action<Vector3> ProjectileLoaded; 
    public event Action ProjectileUnloaded;

    void Start()
    {
        _slingSourcePosition = slingProjectileSource.position;
        _ammo = new Dictionary<Projectile, int>();
    }

    public Vector3 SlingSourcePosition => _slingSourcePosition;

    public Projectile LoadedProjectile => loadedProjectile;

    public float ZOffset => zOffset;

    private void FixedUpdate()
    {
        if (_shouldShoot)
        {
            _shouldShoot = false;
            loadedProjectile.Rigidbody.isKinematic = false;
            Vector3 shotDirection = _slingSourcePosition - loadedProjectile.transform.position;
            loadedProjectile.Rigidbody.AddForce(shotDirection * shotMultiplier, ForceMode.Impulse); //TODO: Add projectile multipliers
            OnProjectileUnloaded();
        }
    }

    public void Shoot()
    {
        _shouldShoot = true;
    }
    
    public  void EnforceSlingConstraints(Vector3 mouseWorldPosition)
    {
        Vector3 slingOffset = mouseWorldPosition - SlingSourcePosition;
        if (slingOffset.magnitude > maxDragDistance)
        {
            mouseWorldPosition = _slingSourcePosition + slingOffset.normalized * maxDragDistance;
        }

        if (mouseWorldPosition.y > _slingSourcePosition.y)
        {
            mouseWorldPosition.y = _slingSourcePosition.y;
        }

        if (mouseWorldPosition.z > _slingSourcePosition.z - zOffset)
        {
            mouseWorldPosition.z = _slingSourcePosition.z - zOffset;
        }

        loadedProjectile.transform.position = mouseWorldPosition;
    }

    public void SwitchProjectile()
    {
        throw new System.NotImplementedException();
    }

    public void ResetProjectile()
    {
        loadedProjectile.transform.position = SlingSourcePosition;
        loadedProjectile.GetComponent<Rigidbody>().velocity = Vector3.zero;
        loadedProjectile.GetComponent<Rigidbody>().isKinematic = true;
    }

    public void OnProjectileLoaded()
    {
        ProjectileLoaded?.Invoke(loadedProjectile.transform.position);
    }
    
    public void OnProjectileUnloaded()
    {
        ProjectileUnloaded?.Invoke();
    }
}
