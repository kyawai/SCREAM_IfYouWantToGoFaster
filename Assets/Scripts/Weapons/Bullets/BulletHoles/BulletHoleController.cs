using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

/// <summary>
/// When bullet hole decal has been spawned at collision point,
/// fade it out and destroy it
/// </summary>
public class BulletHoleController : MonoBehaviour 
{
    [Tooltip("Decal projector for this object")]
    [SerializeField]
    private DecalProjector _projector;
    private IBulletHoleFactory _bulletHolePool;

    private void OnEnable()
    {   
        StartCoroutine(BulletHoleTimer());
    }

    public void SetBulletHolePool(IBulletHoleFactory bulletHolePool)
    {
        _bulletHolePool = bulletHolePool;
    }

    private IEnumerator BulletHoleTimer()
    {
        float time = 1f;
        while (time > 0f)
        {
            time -= Time.deltaTime;
            _projector.fadeFactor = time;
            yield return new WaitForSecondsRealtime(0.001f);
        }
        _bulletHolePool.ReturnBulletHole(gameObject);

    }
}
