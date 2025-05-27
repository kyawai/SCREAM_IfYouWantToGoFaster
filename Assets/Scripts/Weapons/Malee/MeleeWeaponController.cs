using System.Collections;
using UnityEngine;

public class MeleeWeaponController : ApplyWeaponComponents
{
    [SerializeField] private GameObject _brokenWeapon;
    private bool _canBreak = false;

    private void OnCollisionEnter(Collision collision)
    {
        if (_canBreak)
        {
            Break();
        }
    }

    private void Break()
    {
        _brokenWeapon.transform.parent = null;
        _brokenWeapon.SetActive(true);
        this.gameObject.SetActive(false);
    }
    private IEnumerator WaitToDestroy()
    {
        yield return new WaitForSeconds(5f);
        //Destroy(gameObject);
    }

    public void SetCanBreak(bool canBreak)
    {
        _canBreak = canBreak;
    }
}
