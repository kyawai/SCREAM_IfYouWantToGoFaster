using System;
using System.Collections;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

/// <summary>
/// Abstract script for base gun controller
/// </summary>
[RequireComponent(typeof(AudioSource))]
public abstract class GunController : MonoBehaviour, IShootable
{
    [Header("Gun")]
    protected Transform _thisGun;
    [SerializeField] protected GunSO gun;
    [SerializeField] protected Transform firePoint;

    [Header("Audio")]
    private AudioSource _audioSoure;

    [Header("Bullets")]
    private IBulletFactory _bulletFactory;
    [SerializeField] private GameObject bulletPrefab;

    [Header("Recoil")]
    protected Quaternion _originalRot;


    protected virtual void Start()
    {
        _thisGun = this.gameObject.transform;

        BulletPoolManager.Instance.InitialisePool(bulletPrefab, 50);
        _bulletFactory = BulletPoolManager.Instance.GetBulletPool();

        _audioSoure = GetComponent<AudioSource>();

        Initialise();
    }

    protected virtual void Initialise()
    {
        ResetAmmo();
    }
    public virtual void Shoot()
    {
        _originalRot = _thisGun.localRotation;
        if (HasAmmo()){
            switch (gun.gunType)
            {
                case GunSO.GunType.pistol:
                case GunSO.GunType.sniper:
                case GunSO.GunType.shotgun:
                    ShootOneBullet();
                    break;
                case GunSO.GunType.smg:
                case GunSO.GunType.assultrifle:
                    StartAutomaticFire();
                    break;
            }
        }
    }

    protected virtual void UseAmmo()
    {
        gun.ammo--;
    }
    protected abstract void StartAutomaticFire();
    protected abstract void StopAutomaticFire();
    protected abstract void OnAmmoEmpty();

    protected void ShootOneBullet()
    {
        _audioSoure.PlayOneShot(gun.shootingSound);
        GameObject bullet = _bulletFactory.GetBullet(firePoint.position, firePoint.rotation);
        UseAmmo();

        if (bullet.TryGetComponent(out Rigidbody rb))
        {
            rb.linearVelocity = Vector3.zero;
            rb.AddForce(firePoint.forward * gun.bulletForce, ForceMode.Impulse);
        }
    }


    protected virtual void ApplyRecoil()
    {
        Vector3 recoilRotation = new Vector3(
            -gun.recoilAmount,
            UnityEngine.Random.Range(-gun.recoilAmount * 0.5f, gun.recoilAmount * 0.5f),
            UnityEngine.Random.Range(-gun.recoilAmount * 0.5f, gun.recoilAmount * 0.5f)
        );
        _thisGun.localRotation = _originalRot * Quaternion.Euler(recoilRotation);
        StartCoroutine(ResetRecoil());
    }

    protected IEnumerator ResetRecoil()
    {
        float resetSpeed = 5f;

        while (Quaternion.Angle(_thisGun.localRotation, _originalRot) > 0.1f)
        {
            _thisGun.localRotation = Quaternion.Slerp(_thisGun.localRotation, _originalRot, Time.deltaTime * resetSpeed);
            yield return null;
        }

        _thisGun.localRotation = _originalRot;
    }

    protected virtual bool CanShoot()
    {
        return gun.ammo > 0;
    }
    public int GetAmmo() => gun.ammo;
    public bool HasAmmo() => gun.ammo > 0;
    public void ResetAmmo() => gun.ammo = gun.maxAmmo;

}
