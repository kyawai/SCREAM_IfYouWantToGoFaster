using UnityEngine;

public class MeleeWeaponController : ApplyWeaponComponents
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
