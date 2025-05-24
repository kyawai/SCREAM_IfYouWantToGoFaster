using System.Collections;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

[RequireComponent(typeof(AudioSource))]
public class GunController : ApplyWeaponComponents, IShootable
{
    public GunSO gun;
    public Transform firePoint;
    public GameObject bulletPrefab;
    private AudioSource _audioSoure;

    private IBulletFactory _bulletFactory;

    private Quaternion _originalRot;
    private bool _isTriggerDown;

    private Transform _thisGun;

    //private bool _hasLaser;
    //[SerializeField] private LineRenderer _lineRenderer;
    //[SerializeField] private LayerMask _layerMask;

    private void Start()
    {
        _thisGun = this.gameObject.transform;

        BulletPoolManager.Instance.InitialisePool(bulletPrefab, 50);
        _bulletFactory = BulletPoolManager.Instance.GetBulletPool();

        _audioSoure = GetComponent<AudioSource>();
        gun.ammo = 20; //DELETE AFTER 

        //if (_hasLaser)
        //{ 

        //    _originalRot = _thisGun.localRotation;
        //    _lineRenderer.positionCount = 2;
        //    _lineRenderer.startWidth = 0.01f;
        //    _lineRenderer.endWidth = 0.01f;
        //}

    }
    public void Shoot()
    {
        if (gun.ammo <= 0) return;
        switch (gun.gunType)
        {
            case GunSO.GunType.pistol:
                ShootOneBullet();
                return;
            case GunSO.GunType.sniper:
                //EnableLaser(_hasLaser);
                ShootOneBullet();
                return;
            case GunSO.GunType.shotgun:
                ShootOneBullet();
                return;
            case GunSO.GunType.smg:
                StartCoroutine(ShootContineousBullet());
                return;
            case GunSO.GunType.assultrifle:
                StartCoroutine(ShootContineousBullet());
                return;
        }
    }

    public void TriggerPressedDown(bool triggerDown)
    {
        _isTriggerDown = triggerDown;
    }

    private void ShootOneBullet()
    {
        _audioSoure.PlayOneShot(gun.shootingSound);
        GameObject bullet = _bulletFactory.GetBullet(firePoint.position, firePoint.rotation);
        gun.ammo--;
        if (bullet.TryGetComponent(out Rigidbody rb))
        {
            rb.linearVelocity = Vector3.zero;
            rb.AddForce(firePoint.forward * gun.bulletForce, ForceMode.Impulse);
        }
    }

    private IEnumerator ShootContineousBullet()
    {
        float elapsed = 0f;
        float recoilAmountLow = 0f;
        float recoilAmountHigh = 0f;
        while (_isTriggerDown && gun.ammo > 0)
        {
            _audioSoure.PlayOneShot(gun.shootingSound);
            GameObject bullet = _bulletFactory.GetBullet(firePoint.position, firePoint.rotation);
            gun.ammo--;
            if (bullet.TryGetComponent(out Rigidbody rb))
            {
                rb.linearVelocity = Vector3.zero;
                rb.AddForce(firePoint.forward * gun.bulletForce, ForceMode.Impulse);
            }
            float lerpFactor = elapsed / (gun.ammo * 0.1f);

            recoilAmountLow = recoilAmountLow - gun.recoilAmount;
            recoilAmountHigh = recoilAmountHigh + gun.recoilAmount;
            Vector3 randomRotation = new Vector3(0f, Random.Range(recoilAmountLow, recoilAmountHigh), Random.Range(recoilAmountLow,recoilAmountHigh));
            // _thisGun.localRotation = Quaternion.Slerp(_originalRot, _originalRot * Quaternion.Euler(randomRotation),lerpFactor);
            elapsed += Time.deltaTime;
            
            yield return new WaitForSeconds(0.1f);

        }
        _thisGun.localRotation = _originalRot;
    }

    //public void EnableLaser(bool activateLaser)
    //{
    //    _hasLaser = activateLaser;
    //}

    //private void Update()
    //{
    //    if (_hasLaser)
    //    {
    //        Ray ray = new Ray(firePoint.position, firePoint.forward);
    //        RaycastHit hit;
    //        Vector3 end;
    //        if (Physics.Raycast(ray, out hit, 100f, _layerMask))
    //        {
    //            end = hit.point;
    //        }
    //        else
    //        {
    //            end = firePoint.position + firePoint.forward * 100f;
    //        }
    //        _lineRenderer.SetPosition(0, firePoint.position);
    //        //_lineRenderer.SetPosition(1, (firePoint.transform.forward * 100f));
    //        _lineRenderer.SetPosition(1, end);
    //    }
    //}
    private void ApplyRecoil()
    {

    }

}
