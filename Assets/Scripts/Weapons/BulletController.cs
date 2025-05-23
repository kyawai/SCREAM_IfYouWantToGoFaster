using UnityEngine;

/// <summary>
/// Controls the bullet time through the air/if it collides and returns it to the pool
/// </summary>
public class BulletController : MonoBehaviour
{
    private IBulletFactory _bulletPool;
    private readonly float _lifeTime = 5f;
    private float _lifeTimer;

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
        if (collision.gameObject.CompareTag("Gun"))
        { return; }
        else
        {
            ReturnToPool();
        }
    }

    private void ReturnToPool()
    {
        if (_bulletPool != null)
        {
            _bulletPool.ReturnBullet(gameObject);
        }
    }

}
