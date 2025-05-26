using System.Collections;
using UnityEngine;

public class PlayerGunController : GunController
{
    [Header("VR Input")]
    private bool _isTriggerDown;
    private Coroutine _automaticFireCoroutine;

    public void TriggerPressedDown(bool triggerDown)
    {
        _isTriggerDown = triggerDown;
        if (triggerDown)
        {
            Shoot();
        }
        else
        {
            StopAutomaticFire();
        }
    }

    public void DroppedGun()
    {
        if(_isTriggerDown)
        {
            _isTriggerDown = false;
            StopAutomaticFire();
        }
    }
    protected override void StartAutomaticFire()
    {
        if (_automaticFireCoroutine == null)
        {
            _automaticFireCoroutine = StartCoroutine(ContinuousShooting());
        }
    }
    protected override void StopAutomaticFire()
    {
        if (_automaticFireCoroutine != null)
        {
            {
                StopCoroutine(_automaticFireCoroutine);
                _automaticFireCoroutine = null;

                // Reset recoil
                _thisGun.localRotation = _originalRot;
            }
        }
    }

    private IEnumerator ContinuousShooting()
    {
        float elapsed = 0f;
        float recoilAccumulation = 0f;
        while (_isTriggerDown && CanShoot())
        {
            ShootOneBullet();

            // Progressive recoil for automatic weapons
            recoilAccumulation += gun.recoilAmount * 0.1f;
            elapsed += Time.deltaTime;

            yield return new WaitForSeconds(0.1f);
        }

        _automaticFireCoroutine = null;

        // Smoothly return to original rotation
        StartCoroutine(ResetFullRecoil());
    }

    private IEnumerator ResetFullRecoil()
    {
        float resetSpeed = 3f;

        while (Quaternion.Angle(_thisGun.localRotation, _originalRot) > 0.1f)
        {
            _thisGun.localRotation = Quaternion.Slerp(_thisGun.localRotation, _originalRot, Time.deltaTime * resetSpeed);
            yield return null;
        }

        _thisGun.localRotation = _originalRot;
    }

    // Override recoil for more dramatic player feedback
    protected override void ApplyRecoil()
    {
        Vector3 recoilRotation = new Vector3(
            -gun.recoilAmount * 1.5f, // More dramatic for player
            Random.Range(-gun.recoilAmount, gun.recoilAmount),
            Random.Range(-gun.recoilAmount * 0.5f, gun.recoilAmount * 0.5f)
        );

        _thisGun.localRotation = _originalRot * Quaternion.Euler(recoilRotation);
    }

    protected override void OnAmmoEmpty()
    {
        Debug.Log("Empty ammo");
    }



}
