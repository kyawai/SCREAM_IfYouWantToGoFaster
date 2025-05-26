using System.Collections;
using UnityEngine;
using UnityEngine.LowLevel;

public class EnemyGunController : GunController
{
    [Header("AI Settings")]
    [SerializeField] private float _cooldownBetweenShots = 1f;
    [SerializeField] private float _aimAccuracy = 0.8f;

    private Coroutine _automaticFireCoroutine;
    private bool _isAiming;
    private Transform _playerHead;

    [SerializeField]private EnemyAlertController _alertController;

    protected override void Initialise()
    {
        _alertController.OnPlayerDetected.AddListener(StartAiming);
        _alertController.OnPlayerLost.AddListener(StopAiming);

    }
    private void OnDestroy()
    {
        if (_alertController != null)
        {
            _alertController.OnPlayerDetected.RemoveListener(StartAiming);
            _alertController.OnPlayerLost.RemoveListener(StopAiming);
        }
    }
    protected override bool CanShoot() { return true; }
    protected override void UseAmmo()
    {

    }

    public void StartAiming(Transform playerHead)
    {
        _isAiming = true;
        _playerHead = playerHead;
        StartAutomaticFire();
    }

    public void StopAiming()
    {
        _isAiming = false;
        StopAutomaticFire();

    }
    protected override void OnAmmoEmpty()
    {
        
    }

    protected override void StartAutomaticFire()
    {
        if (_automaticFireCoroutine == null && _isAiming)
        {
            _automaticFireCoroutine = StartCoroutine(AIShoot());
        }

    }

    protected override void StopAutomaticFire()
    {
        if (_automaticFireCoroutine != null)
        {
            StopCoroutine(_automaticFireCoroutine);
            _automaticFireCoroutine = null;
        }
    }

    private IEnumerator AIShoot()
    {
        float burstStartTime = Time.time;
        
        while (_isAiming)
        {
            // Aim at target with some inaccuracy
            if (_playerHead != null)
            {
                AimAtTarget();
            }
            ShootOneBullet();
            yield return new WaitForSeconds(1f); // Slightly slower than player
        }

        // Cooldown between bursts
        yield return new WaitForSeconds(_cooldownBetweenShots);

        _automaticFireCoroutine = null;

        // Continue firing if still aiming
        if (_isAiming)
        {
            StartAutomaticFire();
        }
    }

    protected override void ApplyRecoil()
    {
    }

    private void AimAtTarget()
    {
        if (_playerHead == null) return;

        Vector3 directionToTarget = (_playerHead.position - firePoint.position).normalized;

        // Add some inaccuracy based on AI accuracy setting
        Vector3 inaccuracy = new Vector3(
            Random.Range(-1f, 1f) * (1f - _aimAccuracy),
            Random.Range(-1f, 1f) * (1f - _aimAccuracy),
            Random.Range(-1f, 1f) * (1f - _aimAccuracy)
        );

        //directionToTarget += inaccuracy * 0.1f;
        firePoint.rotation = Quaternion.LookRotation(directionToTarget);
    }
}
