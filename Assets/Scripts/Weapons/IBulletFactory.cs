using UnityEngine;

public interface IBulletFactory
{
    GameObject GetBullet(Vector3 position, Quaternion rotation);
    void ReturnBullet(GameObject bullet);
}
