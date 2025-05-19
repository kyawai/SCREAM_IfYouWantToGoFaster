using UnityEngine;

public class MeleeWeapon : WeaponBase
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Breakable") || collision.gameObject.CompareTag("Enemy"))
        {
            Break();
        }
    }

    private void Break()
    {
        Destroy(gameObject);
    }
}
