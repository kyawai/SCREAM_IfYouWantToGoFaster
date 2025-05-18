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
            if (_instance == null)
            {
                GameObject managerObject = new GameObject("Bullet Pool Manager");
                _instance = managerObject.AddComponent<BulletPoolManager>();
                DontDestroyOnLoad(managerObject);
            }
            return _instance;
        }
    }

    ////********MIGHT NOT NEED*******
    ////Dictionary to store bullet pools by prefab ID
    //private Dictionary<int, BulletPool> bulletPools = new Dictionary<int, BulletPool>();

    ////********MIGHT NOT NEED*******
    ////Get or create a pool for a specific bullet prefab
    //public BulletPool GetBulletPool(GameObject bulletPrefab, int maxPoolSize)
    //{
    //    int prefabID = bulletPrefab.GetInstanceID();
    //    if(!bulletPools.ContainsKey(prefabID))
    //    {
    //        BulletPool newPool = new BulletPool(bulletPrefab, maxPoolSize);
    //        bulletPools.Add(prefabID, newPool);
    //    }
    //    return bulletPools[prefabID];
    //}


}
