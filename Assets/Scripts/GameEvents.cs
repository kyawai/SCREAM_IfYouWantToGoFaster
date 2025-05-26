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
        public string hitTag;

        public BulletHitData(String hit)
        {
            hitTag = hit;
        }
        public override string ToString()
        {
            return hitTag.ToString();
        }
        public string GetHitData()
        {
            return hitTag;
        }
    }
}
