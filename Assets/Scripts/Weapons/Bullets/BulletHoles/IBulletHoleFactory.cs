using UnityEngine;

public interface IBulletHoleFactory
{
    GameObject GetBulletHole(Vector3 position, Quaternion rotation, Transform parent);
    void ReturnBulletHole(GameObject bullet);
}
