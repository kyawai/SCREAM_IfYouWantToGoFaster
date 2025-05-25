using UnityEngine;

public class EnemyController : MonoBehaviour 
{
    public EnemySO enemySO;
    protected Transform player;
    protected IEnemyState currentState;
    protected IEnemyMovement currentMovement;
   
    protected void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;

        IEnemyMovement currentMovement = GetComponent<IEnemyMovement>();
        IEnemyState currentState = GetComponent<IEnemyState>();

        SetState(new PatrolState());

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
    public void SetMovement(IEnemyMovement movement)
    {
        currentMovement = movement;
        Debug.Log("SETTING MOVEMENT");
    }

    public IEnemyMovement GetMovement()
    {
        return currentMovement;
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
