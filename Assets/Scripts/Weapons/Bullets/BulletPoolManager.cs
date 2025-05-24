using System.Collections.Generic;
using UnityEngine;

public class BulletPoolManager : MonoBehaviour
{
    /// <summary>
    /// Create this manager as a singleton to control all bullets in the level for memory management
    /// </summary>
    private static BulletPoolManager _instance;


    public static BulletPoolManager Instance
    {
        get
        {
            if (_instance == null && Application.isPlaying)
            {
                GameObject managerObject = new GameObject("Bullet Pool Manager");
                _instance = managerObject.AddComponent<BulletPoolManager>();
                 DontDestroyOnLoad(managerObject);
            }
            return _instance;
        }
    }

    private BulletPool _globalBulletPool;


    public void InitialisePool(GameObject bulletPrefab, int maxPoolSize)
    {
        if (_globalBulletPool == null)
        {
            _globalBulletPool = new BulletPool(bulletPrefab, maxPoolSize);
        }
    }

    public BulletPool GetBulletPool()
    {
        return _globalBulletPool;
    }


}
