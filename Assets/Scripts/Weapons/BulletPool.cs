using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Creates the Bullet pool by instantiating bullets, getting the bullets when required and
/// returning them back to the pool (Called by bullet controller)
/// </summary>
public class BulletPool : IBulletFactory
{
    private readonly GameObject _bulletPrefab;
    private readonly Queue<GameObject> _availableBullets = new Queue<GameObject>(); //Create a queue for the bullets to go from/go back
    private readonly List<GameObject> _activeBullets = new List<GameObject>();
    private readonly Transform _poolContainer;
    private readonly int _maxPoolSize; 
    private float gameTime;

    public BulletPool(GameObject bulletPrefab, int maxPoolSize)
    {
        this._bulletPrefab = bulletPrefab;
        this._maxPoolSize = maxPoolSize;

        GameObject container = new GameObject($"BulletPool_{bulletPrefab.name}");
        //Object.DontDestroyOnLoad( container );
        _poolContainer = container.transform;

        InitialisePool();
    }

    private void InitialisePool()
    {
        for (int i = 0; i < _maxPoolSize; i++)
        {
            GameObject bullet = Object.Instantiate(_bulletPrefab, _poolContainer); //Instatiate the bullet

            BulletController bulletController = bullet.GetComponent<BulletController>();
            bulletController.SetBulletPool(this);

            bullet.SetActive(false); //Set it to be invisible
            _availableBullets.Enqueue(bullet); //Put it in the queue
        }
    }

    public void UpdateTime()
    {
        gameTime = Time.time;
    }

    public GameObject GetBullet(Vector3 position, Quaternion rotation)
    {
        if (_availableBullets.Count <= 0)
        {
            Debug.Log("No Bullets available in pool!");
            return null;
        }

        GameObject bullet = _availableBullets.Dequeue(); //set the game object bullet as the bullet in the queue
        bullet.transform.position = position;
        bullet.transform.rotation = rotation;

        _activeBullets.Add(bullet);
        bullet.SetActive(true);
        return bullet;
    }

    public void ReturnBullet(GameObject bullet)
    {
        if (bullet == null) return;

        //Reset and deactive
        bullet.SetActive(false);
        bullet.transform.parent = _poolContainer;

        //remove from active ditionary and add to queue
        _activeBullets.Remove(bullet);
        _availableBullets.Enqueue(bullet);
    }

}
