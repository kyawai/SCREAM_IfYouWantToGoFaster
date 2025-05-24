using UnityEngine;
using static UnityEditor.ShaderGraph.Internal.KeywordDependentCollection;

public class BulletHolePoolManager : MonoBehaviour
{
    /// <summary>
    /// Create this manager as a singleton to control all bullet holes in the level for memory management
    /// </summary>
    /// 

    private static BulletHolePoolManager _instance;
    public static BulletHolePoolManager Instance
    {
        get
        {
            if (_instance == null && Application.isPlaying)
            {
                GameObject managerObject = new GameObject("Bullet Hole Pool Manager");
                _instance = managerObject.AddComponent<BulletHolePoolManager>();
                DontDestroyOnLoad(managerObject);
            }
            return _instance;
        }
    }
         private BulletHolePool _globalBulletHolePool;


    public void InitialisePool(GameObject bulletHolePrefab, int maxPoolSize)
    {
        if (_globalBulletHolePool == null)
        {
            _globalBulletHolePool = new BulletHolePool(bulletHolePrefab, maxPoolSize);
        }
    }

    public BulletHolePool GetBulletHolePool()
    {
        return _globalBulletHolePool;
    }
}

