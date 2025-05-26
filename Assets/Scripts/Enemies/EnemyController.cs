using System;
using UnityEngine;

public class EnemyController : MonoBehaviour 
{
    public EnemySO enemySO;
    protected Transform player;
    protected IEnemyState currentState;
    protected IEnemyMovement currentMovement;
    private EnemyAlertController _alertController;
   
    protected void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;

        currentMovement = GetComponent<IEnemyMovement>();
        IEnemyState currentState = GetComponent<IEnemyState>();

        _alertController = GetComponent<EnemyAlertController>();

        _alertController.OnPlayerDetected.AddListener(PlayerDetected);
        _alertController.OnPlayerLost.AddListener(PlayerLost);

        SetState(new PatrolState());

        GameEvents.OnBulletHit += Die;
    }


    private void OnDestroy()
    {
        GameEvents.OnBulletHit -= Die;
        if (_alertController != null)
        {
            _alertController.OnPlayerDetected.RemoveListener(PlayerDetected);
            _alertController.OnPlayerLost.RemoveListener(PlayerLost);
        }
    }

    private void PlayerDetected(Transform player)
    {
        SetState(new AlertState());
    }

    private void PlayerLost()
    {
        SetState(new PatrolState());
    }
    
    public EnemyAlertController GetEnemyAlertController()
    {
        return _alertController;
    }

    public EnemySO GetEnemySO()
    {
        return enemySO;
    }
    public IEnemyMovement GetMovement()
    {
        return currentMovement;
    }

    protected void Update()
    {
        currentState?.UpdateState(this);
    }
    public void SetState(IEnemyState state)
    {
        currentState?.ExitState(this);
        currentState = state;
        currentState.EnterState(this);
    }

    public virtual void NoiseAlert(Vector3 noisePos)
    {
        SetState(new AlertState());
    }
    protected virtual void Attack()
    {
        Debug.Log(enemySO.enemyType + "IS ATTACKING");
    }
    public void Die(GameEvents.BulletHitData hitData)
    {
        if (hitData.ToString() == "Enemy")
        {
            Destroy(gameObject);
        }
    }
}
