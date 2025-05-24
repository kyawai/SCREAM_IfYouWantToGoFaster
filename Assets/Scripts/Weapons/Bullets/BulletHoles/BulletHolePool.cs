using System.Collections.Generic;
using UnityEngine;

public class BulletHolePool : IBulletHoleFactory
{
    private readonly GameObject _bulletHolePrefab;
    private readonly Queue<GameObject> _availableBulletHoles = new Queue<GameObject>();
    private readonly List<GameObject> _activeBulletHoles = new List<GameObject>();
    private readonly Transform _poolContainer;
    private readonly int _maxPoolSize;

    public BulletHolePool(GameObject bulletHolePrefab, int maxPoolSize)
    {
        this._bulletHolePrefab = bulletHolePrefab;
        this._maxPoolSize = maxPoolSize;

        GameObject container = new GameObject($"BulletHolePool_{bulletHolePrefab.name}");
        _poolContainer = container.transform;

        InitlaisePool();
    }

    private void InitlaisePool()
    {
        for (int i = 0; i < _maxPoolSize; i++)
        {
            GameObject bulletHole = Object.Instantiate(_bulletHolePrefab, _poolContainer); 
            
            BulletHoleController bulletHoleController = bulletHole.GetComponent<BulletHoleController>();
            bulletHoleController.SetBulletHolePool(this);
            
            bulletHole.SetActive(false);
            _availableBulletHoles.Enqueue(bulletHole);
        }
    }

    public GameObject GetBulletHole(Vector3 position, Quaternion rotation, Transform parent)
    {
        if (_availableBulletHoles.Count <= 0)
        {
            Debug.Log("No bullet holes available in pool!");
            return null;
        }

        GameObject bulletHole = _availableBulletHoles.Dequeue();
        bulletHole.transform.position = position;
        bulletHole.transform.rotation = rotation;
        bulletHole.transform.SetParent(parent);
        bulletHole.SetActive(true);

        _activeBulletHoles.Add(bulletHole);
        return bulletHole;
    }

    public void ReturnBulletHole(GameObject bulletHole)
    {
        if (bulletHole == null) return;

        bulletHole.SetActive(false);
        bulletHole.transform.parent = _poolContainer;

        _activeBulletHoles.Remove(bulletHole);
        _availableBulletHoles.Enqueue(bulletHole);
    }

}
