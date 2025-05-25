using UnityEngine;

/// <summary>
/// Controls the bullet time through the air/if it collides and returns it to the pool
/// </summary>
public class BulletController : MonoBehaviour
{
    private IBulletFactory _bulletPool;

    private BulletHolePool _bulletHolePool;


    private readonly float _lifeTime = 5f;
    private float _lifeTimer;

    [SerializeField] private GameObject _bulletHolePrefab;

    private void Start()
    {
        BulletHolePoolManager.Instance.InitialisePool(_bulletHolePrefab, 50); ;
        _bulletHolePool = BulletHolePoolManager.Instance.GetBulletHolePool();
    }
    public void SetBulletPool(IBulletFactory pool)
    {
        _bulletPool = pool;
    }

    private void OnEnable()
    {
        _lifeTimer = 0f;
    }

    private void Update()
    {
        _lifeTimer += Time.deltaTime;
        if (_lifeTimer >= _lifeTime) { ReturnToPool(); }
    }
    private void OnCollisionEnter(Collision collision)
    {
        SpawnBulletHole(collision);
        ReturnToPool();
    }

    private void ReturnToPool()
    {
        if (_bulletPool != null)
        {
            _bulletPool.ReturnBullet(gameObject);
        }
    }

    private void SpawnBulletHole(Collision collision)
    {
        ContactPoint contactPoint = collision.contacts[0];
        Quaternion bulletHoleRot = Quaternion.LookRotation(-contactPoint.normal, Vector3.up); 

        GameObject bulletHole = _bulletHolePool.GetBulletHole(contactPoint.point, bulletHoleRot, collision.gameObject.transform);
        
    }
}
