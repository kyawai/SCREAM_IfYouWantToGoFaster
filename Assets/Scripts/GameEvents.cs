using UnityEngine;
using System;

//Global Event Manager
public static class GameEvents
{
    public static event Action<BulletHitData> OnBulletHit;
    public static void TriggerBulletHit(BulletHitData hitData)
    {
        OnBulletHit?.Invoke(hitData);
    }

    //Data structure
    [System.Serializable]
    public struct BulletHitData
    {
        public GameObject hitObject;

        public BulletHitData(GameObject hit)
        {
            hitObject = hit;
        }

        public GameObject GetHitData()
        {
            return hitObject;
        }
    }
}
