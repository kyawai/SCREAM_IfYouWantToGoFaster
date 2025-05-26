using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    private void Awake()
    {
        GameEvents.OnBulletHit += Die;
    }

    private void OnDestroy()
    {
        GameEvents.OnBulletHit -= Die;
    }
    private void Die(GameEvents.BulletHitData hitData)
    {
        if (hitData.ToString() == "Player")
        {
            Debug.Log("PLAYER DEAD");
        }
    }
}
