using UnityEngine;

public class EnemyController : MonoBehaviour 
{
    public EnemySO enemySO;
    protected Transform player;
    protected IEnemyState currentState;
    protected IEnemyMovement currentMovement;
   
    protected void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;

        currentMovement = GetComponent<IEnemyMovement>();
        IEnemyState currentState = GetComponent<IEnemyState>();


        SetState(new PatrolState());
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
        currentState = state;
        currentState.EnterState(this);
    }

    public virtual void NoiseAlert(Vector3 noisePos)
    {
        Debug.Log("HEARD PLAYER");
        SetState(new AlertState());
    }
    protected virtual void Attack()
    {
        Debug.Log(enemySO.enemyType + "IS ATTACKING");
    }
    public void Die()
    {
        Debug.Log("DEAD");
        Destroy(gameObject);
    }
}
