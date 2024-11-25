using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;

public class SlingManager : MonoBehaviour
{
    [SerializeField] private Projectile loadedProjectile;
    private List<Projectile> _projectilePool;
    public Transform slingProjectileSource;
    private Vector3 _slingSourcePosition;
    private bool _shouldShoot;
    private ProjectileSimulator _simulator;
    [SerializeField] private float maxDragDistance = 4f;
    [SerializeField] private float zOffset = 1f;
    [SerializeField] private float forceMultiplier = 5f;

    public float ForceMultiplier => forceMultiplier;
    public event Action<Projectile> ProjectileLoaded; 
    public event Action ProjectileUnloaded;

    // private const string _projectilesFolderPath = "Assets/Scripts/Projectiles";
    
    void Start()
    {
        _projectilePool = new List<Projectile>();
        _simulator = GetComponent<ProjectileSimulator>();
        _slingSourcePosition = slingProjectileSource.position;
    }

    /*
     * After some thought I wanted to load all the prefabs into the sling manager into a list,
     * and iterating to the next projectile when the user right clicks.
     * didn't have the time.
     * I understood that The solution I wanted to implement using iteration
     * over scripts wouldn't work because there will not be a gameObject
     */
    
    
    // private void Awake()
    // {
    //     string[] scriptGUIDs = AssetDatabase.FindAssets("t:Script", new[] { _projectilesFolderPath });
    //
    //     foreach (string guid in scriptGUIDs)
    //     {
    //         string scriptPath = AssetDatabase.GUIDToAssetPath(guid);
    //         MonoScript monoScript = AssetDatabase.LoadAssetAtPath<MonoScript>(scriptPath);
    //
    //         if (monoScript != null)
    //         {
    //             // Get the class type of the script
    //             Type scriptType = monoScript.GetClass();
    //
    //             if (scriptType != null && scriptType.IsSubclassOf(typeof(Projectile)))
    //             {
    //                 // Create a new GameObject and attach the script
    //                 GameObject projectileObject = new GameObject(scriptType.Name);
    //                 Projectile projectile = (Projectile)projectileObject.AddComponent(scriptType);
    //                 Debug.Log($"Created GameObject with script: {scriptType.Name}");
    //                 // _projectilePool.Add(projectile);
    //             }
    //         }
    //     }
    // }

    public Vector3 SlingSourcePosition => _slingSourcePosition;

    public Projectile LoadedProjectile => loadedProjectile;

    public float ZOffset => zOffset;

    private void FixedUpdate()
    {
        if (_shouldShoot)
        {
            _shouldShoot = false;
            loadedProjectile.Rigidbody.isKinematic = false;
            loadedProjectile.IsFlying = true;
            Vector3 shotDirection = _slingSourcePosition - loadedProjectile.transform.position;
            loadedProjectile.Rigidbody.AddForce(shotDirection * forceMultiplier, ForceMode.Impulse);
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

    public void ResetProjectile() // TEMP
    {
        loadedProjectile.transform.position = SlingSourcePosition;
        
        // Inefficient, this was for testing purposes
        loadedProjectile.GetComponent<Rigidbody>().velocity = Vector3.zero;
        loadedProjectile.GetComponent<Rigidbody>().isKinematic = true;
        loadedProjectile.ResetProperties();
    }

    public void OnProjectileLoaded()
    {
        ProjectileLoaded?.Invoke(loadedProjectile);
    }
    
    public void OnProjectileUnloaded()
    {
        ProjectileUnloaded?.Invoke();
    }
}
