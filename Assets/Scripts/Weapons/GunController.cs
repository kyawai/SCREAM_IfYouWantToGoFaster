using System.Collections;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

[RequireComponent(typeof(AudioSource))]
public class GunController : WeaponBase, IShootable
{
    public GunSO gun;
    public Transform firePoint;
    public GameObject bulletPrefab;

    private AudioSource _audioSoure;

    private IBulletFactory _bulletFactory;

    private Quaternion _originalRot;
    private bool _isTriggerDown;

    public Transform thisGun;


    public LineRenderer _lineRenderer;
    private bool _hasLineRenderer;
    public LayerMask _layerMask;

    private void Start()
    {
        _bulletFactory = new BulletPool(bulletPrefab, 50);
        _audioSoure = GetComponent<AudioSource>();
        gun.ammo = 20; //DELETE AFTER 

        _originalRot = thisGun.localRotation;
        _lineRenderer.positionCount = 2;
        _lineRenderer.startWidth = 0.01f;
        _lineRenderer.endWidth = 0.01f;
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
                EnableLaser(_hasLineRenderer);
                ShootOneBullet();
                return;
            case GunSO.GunType.shotgun:
                ShootOneBullet();
                return;
            case GunSO.GunType.smg:
                _hasLineRenderer = true;
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
            Debug.Log("LERP FACTOR" + lerpFactor);

            recoilAmountLow = recoilAmountLow - gun.recoilAmount;
            recoilAmountHigh = recoilAmountHigh + gun.recoilAmount;
            Vector3 randomRotation = new Vector3(0f, Random.Range(recoilAmountLow, recoilAmountHigh), Random.Range(recoilAmountLow,recoilAmountHigh));
            Debug.Log("recoil low: " + recoilAmountLow);
             thisGun.localRotation = Quaternion.Slerp(_originalRot, _originalRot * Quaternion.Euler(randomRotation),lerpFactor);
            elapsed += Time.deltaTime;
            
            yield return new WaitForSeconds(0.1f);

        }
        thisGun.localRotation = _originalRot;
    }

    public void EnableLaser(bool activateLaser)
    {
        _hasLineRenderer = activateLaser;
    }

    private void Update()
    {
        if (_hasLineRenderer)
        {
            Ray ray = new Ray(firePoint.position, firePoint.forward);
            RaycastHit hit;
            Vector3 end;
            if(Physics.Raycast(ray, out hit, 100f, _layerMask))
            {
                end = hit.point;
            }
            else
            {
                end = firePoint.position + firePoint.forward * 100f;
            }
            _lineRenderer.SetPosition(0,firePoint.position);
            //_lineRenderer.SetPosition(1, (firePoint.transform.forward * 100f));
            _lineRenderer.SetPosition(1, end);
        }
    }
    private void ApplyRecoil()
    {

    }

}
